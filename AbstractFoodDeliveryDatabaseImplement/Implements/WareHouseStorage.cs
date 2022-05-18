using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstractFoodDeliveryDatabaseImplement.Implements
{
    public class WareHouseStorage : IWareHouseStorage
    {
        public void Delete(WareHouseBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            WareHouse element = context.WareHouses.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.WareHouses.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractFoodDeliveryDatabase();
            var warehouse = context.WareHouses
                .Include(rec => rec.WareHouseIngredients)
                .ThenInclude(rec => rec.Ingredient)
                .FirstOrDefault(rec => rec.WareHouseName == model.WareHouseName ||
                rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractFoodDeliveryDatabase();
            return context.WareHouses
                .Include(rec => rec.WareHouseIngredients)
                .ThenInclude(rec => rec.Ingredient)
                .Where(rec => rec.WareHouseName.Contains(model.WareHouseName))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<WareHouseViewModel> GetFullList()
        {
            using var context = new AbstractFoodDeliveryDatabase();
            return context.WareHouses
                .Include(rec => rec.WareHouseIngredients)
                .ThenInclude(rec => rec.Ingredient)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(WareHouseBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                WareHouse wareHouse = new WareHouse()
                {
                    WareHouseName = model.WareHouseName,
                    DateCreate = model.DateCreate,
                    StorekeeperFIO = model.StorekeeperFIO
                };
                context.WareHouses.Add(wareHouse);
                context.SaveChanges();
                CreateModel(model, wareHouse, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool TakeIngredientsInWork(int dishid, int count)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var dishIngredients = context.DishIngredients
                    .Where(x => x.DishId == dishid)
                    .ToList();


                foreach (var dishingredient in dishIngredients)
                {
                    int countNeed = count * dishingredient.Count;
                    var warehouseingredients = context.WareHouseIngredients
                        .Where(x => x.IngredientId == dishingredient.IngredientId)
                        .ToList();
                    foreach (var warehouseingredient in warehouseingredients)
                    {
                        if (countNeed > 0)
                        {
                            if (warehouseingredient.Count <= countNeed)
                            {
                                countNeed -= warehouseingredient.Count;
                                context.WareHouseIngredients.Remove(warehouseingredient);
                            }
                            else
                            {
                                warehouseingredient.Count -= countNeed;
                                countNeed = 0;
                            }
                        }
                        else
                            break;
                    }
                    if (countNeed > 0)
                    {
                        throw new Exception("Недостаточно ингредиентов для блюда");
                    }
                }
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            return true;
        }

        public void Update(WareHouseBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.WareHouses.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private static WareHouse CreateModel(WareHouseBindingModel model, WareHouse wareHouse,
            AbstractFoodDeliveryDatabase context)
        {
            wareHouse.WareHouseName = model.WareHouseName;
            wareHouse.DateCreate = model.DateCreate;
            wareHouse.StorekeeperFIO = model.StorekeeperFIO;
            if (model.Id.HasValue)
            {
                var wareHouseIngredients = context.WareHouseIngredients.Where(rec =>
                rec.WareHouseId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.WareHouseIngredients.RemoveRange(wareHouseIngredients.Where(rec =>
                !model.WareHouseIngredients.ContainsKey(rec.IngredientId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateIngredient in wareHouseIngredients)
                {
                    updateIngredient.Count =
                    model.WareHouseIngredients[updateIngredient.IngredientId].Item2;
                    model.WareHouseIngredients.Remove(updateIngredient.IngredientId);
                }
                context.SaveChanges();
            }

            // добавили новые значения в таблицу WareHouseIngredient
            foreach (var whc in model.WareHouseIngredients)
            {
                context.WareHouseIngredients.Add(new WareHouseIngredient
                {
                    WareHouseId = wareHouse.Id,
                    IngredientId = whc.Key,
                    Count = whc.Value.Item2
                });
                context.SaveChanges();
            }
            return wareHouse;
        }

        private static WareHouseViewModel CreateModel(WareHouse wareHouse)
        {
            return new WareHouseViewModel
            {
                Id = wareHouse.Id,
                WareHouseName = wareHouse.WareHouseName,
                StorekeeperFIO = wareHouse.StorekeeperFIO,
                DateCreate = wareHouse.DateCreate,
                WareHouseIngredients = wareHouse.WareHouseIngredients
                .ToDictionary(recPC => recPC.IngredientId,
                recPC => (recPC.Ingredient?.IngredientName, recPC.Count))
            };
        }
    }
}
