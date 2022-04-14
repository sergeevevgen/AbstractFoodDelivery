using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstractFoodDeliveryDatabaseImplement.Implements
{
    public class DishStorage : IDishStorage
    {
        public void Delete(DishBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            Dish element = context.Dishes.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.Dishes.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public DishViewModel GetElement(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractFoodDeliveryDatabase();
            var dish = context.Dishes
            .Include(rec => rec.DishIngredients)
            .ThenInclude(rec => rec.Ingredient)
            .FirstOrDefault(rec => rec.DishName == model.DishName ||
            rec.Id == model.Id);
            return dish != null ? CreateModel(dish) : null;
        }

        public List<DishViewModel> GetFilteredList(DishBindingModel model)
        {
            if(model == null)
            {
                return null;
            }
            using var context = new AbstractFoodDeliveryDatabase();
            return context.Dishes
            .Include(rec => rec.DishIngredients)
            .ThenInclude(rec => rec.Ingredient)
            .Where(rec => rec.DishName.Contains(model.DishName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<DishViewModel> GetFullList()
        {
            using var context = new AbstractFoodDeliveryDatabase();
            return context.Dishes
            .Include(rec => rec.DishIngredients)
            .ThenInclude(rec => rec.Ingredient)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(DishBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                //Сначала надо создать значение в таблице Dish,
                //а уже потом добавлять внешние ключи в таблицу DishIngredient
                Dish dish = new Dish()
                {
                    DishName = model.DishName,
                    Price = model.Price
                };
                context.Dishes.Add(dish);
                context.SaveChanges();
                CreateModel(model, dish, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(DishBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Dishes.FirstOrDefault(rec => rec.Id ==
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

        private static Dish CreateModel(DishBindingModel model, Dish dish, 
            AbstractFoodDeliveryDatabase context)
        {
            dish.DishName = model.DishName;
            dish.Price = model.Price;
            if (model.Id.HasValue)
            {
                var dishIngredients = context.DishIngredients.Where(rec =>
                rec.DishId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.DishIngredients.RemoveRange(dishIngredients.Where(rec =>
                !model.DishIngredients.ContainsKey(rec.IngredientId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateIngredient in dishIngredients)
                {
                    updateIngredient.Count =
                    model.DishIngredients[updateIngredient.IngredientId].Item2;
                    model.DishIngredients.Remove(updateIngredient.IngredientId);
                }
                context.SaveChanges();
            }

            // добавили новые значения в таблицу DishIngredient
            foreach (var ic in model.DishIngredients)
            {
                context.DishIngredients.Add(new DishIngredient
                {
                    DishId = dish.Id,
                    IngredientId = ic.Key,
                    Count = ic.Value.Item2
                });
                context.SaveChanges();
            }
            return dish;
        }

        private static DishViewModel CreateModel(Dish dish)
        {
            return new DishViewModel
            {
                Id = dish.Id,
                DishName = dish.DishName,
                Price = dish.Price,
                DishIngredients = dish.DishIngredients
                .ToDictionary(recPC => recPC.IngredientId,
                recPC => (recPC.Ingredient?.IngredientName, recPC.Count))
            };
        }
    }
}
