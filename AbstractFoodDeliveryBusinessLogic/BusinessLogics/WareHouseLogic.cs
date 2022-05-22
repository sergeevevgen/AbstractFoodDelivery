using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.BusinessLogicsContracts;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryBusinessLogic.BusinessLogics
{
    public class WareHouseLogic : IWareHouseLogic
    {
        private readonly IWareHouseStorage _wareHouseStorage;

        private readonly IIngredientStorage _ingredientStorage;

        public WareHouseLogic(IWareHouseStorage wareHouseStorage, IIngredientStorage ingredientStorage)
        {
            _wareHouseStorage = wareHouseStorage;
            _ingredientStorage = ingredientStorage;
        }

        public void CreateOrUpdate(WareHouseBindingModel model)
        {
            var element = _wareHouseStorage.GetElement(new WareHouseBindingModel
            {
                WareHouseName = model.WareHouseName
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }

            if (model.Id.HasValue)
            {
                _wareHouseStorage.Update(model);
            }
            else
            {
                _wareHouseStorage.Insert(model);
            }
        }

        public void Delete(WareHouseBindingModel model)
        {
            var element = _wareHouseStorage.GetElement(new WareHouseBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _wareHouseStorage.Delete(model);
        }

        public List<WareHouseViewModel> Read(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return _wareHouseStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<WareHouseViewModel>() { _wareHouseStorage.GetElement(model) };
            }

            return _wareHouseStorage.GetFilteredList(model);
        }

        public void AddIngredient(WareHouseBindingModel model, int ingredientId, int count)
        {
            var warehouse = _wareHouseStorage.GetElement(new WareHouseBindingModel { Id = model.Id });
            if (warehouse == null)
            {
                throw new Exception("Склад не найден");
            }

            var ingredient = _ingredientStorage.GetElement(new IngredientBindingModel { Id = ingredientId });
            if (ingredient == null)
            {
                throw new Exception("Ингредиент не найден");
            }

            if (warehouse.WareHouseIngredients.ContainsKey(ingredient.Id))
            {
                int countNow = warehouse.WareHouseIngredients[ingredient.Id].Item2;
                warehouse.WareHouseIngredients[ingredient.Id] = (ingredient.IngredientName, count + countNow);
            }
            else
            {
                warehouse.WareHouseIngredients.Add(ingredient.Id, (ingredient.IngredientName, count));
            }

            _wareHouseStorage.Update(new WareHouseBindingModel
            {
                Id = warehouse.Id,
                WareHouseName = warehouse.WareHouseName,
                StorekeeperFIO = warehouse.StorekeeperFIO,
                DateCreate = warehouse.DateCreate,
                WareHouseIngredients = warehouse.WareHouseIngredients
            });
        }
    }
}

