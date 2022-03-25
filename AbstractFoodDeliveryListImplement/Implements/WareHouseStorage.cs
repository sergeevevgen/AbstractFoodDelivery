using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryListImplement.Models;

namespace AbstractFoodDeliveryListImplement.Implements
{
    public class WareHouseStorage : IWareHouseStorage
    {
        private readonly DataListSingleton source;

        public WareHouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public void Delete(WareHouseBindingModel model)
        {
            for(int i = 0; i < source.WareHouses.Count; ++i)
            {
                if (source.WareHouses[i].Id == model.Id)
                {
                    source.WareHouses.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var warehouse in source.WareHouses)
            {
                if (warehouse.Id == model.Id || warehouse.WareHouseName ==
                model.WareHouseName)
                {
                    return CreateModel(warehouse);
                }
            }
            return null;
        }

        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<WareHouseViewModel>();
            foreach (var warehouse in source.WareHouses)
            {
                if (warehouse.WareHouseName.Contains(model.WareHouseName))
                {
                    result.Add(CreateModel(warehouse));
                }
            }
            return result;
        }

        public List<WareHouseViewModel> GetFullList()
        {
            var result = new List<WareHouseViewModel>();
            foreach (var warehouse in source.WareHouses)
            {
                result.Add(CreateModel(warehouse));
            }
            return result;
        }

        public void Insert(WareHouseBindingModel model)
        {
            var tempWareHouse = new Warehouse
            {
                Id = 1,
                WareHouseIngredients = new Dictionary<int, int>()
            };
            foreach (var warehouse in source.WareHouses)
            {
                if (warehouse.Id >= tempWareHouse.Id)
                {
                    tempWareHouse.Id = warehouse.Id + 1;
                }
            }
            source.Dishes.Add(CreateModel(model, tempWareHouse));
        }

        public void Update(WareHouseBindingModel model)
        {
            Warehouse tempWareHouse = null;
            foreach (var warehouse in source.WareHouses)
            {
                if (warehouse.Id == model.Id)
                {
                    tempWareHouse = warehouse;
                    break;
                }
            }

            if (tempWareHouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempWareHouse);
        }

        private static Warehouse CreateModel(WareHouseBindingModel model,
        Warehouse warehouse)
        {
            warehouse.WareHouseName = model.WareHouseName;
            warehouse.StorekeeperFIO = model.StorekeeperFIO;
            warehouse.DateCreate = model.DateCreate;
            
            // удаляем убранные
            foreach (var key in warehouse.WareHouseIngredients.Keys.ToList())
            {
                if (!model.WareHouseIngredients.ContainsKey(key))
                {
                    warehouse.WareHouseIngredients.Remove(key);
                }
            }
            // обновляем существующие и добавляем новые
            foreach (var ingredient in model.WareHouseIngredients)
            {
                if (warehouse.WareHouseIngredients.ContainsKey(ingredient.Key))
                {
                    warehouse.WareHouseIngredients[ingredient.Key] =
                    model.WareHouseIngredients[ingredient.Key];
                }
                else
                {
                    warehouse.WareHouseIngredients.Add(ingredient.Key,
                    model.WareHouseIngredients[ingredient.Key]);
                }
            }
            return warehouse;
        }

        private static WareHouseViewModel CreateModel(WareHouseBindingModel model)
        {
            var warehouseIngredients = new Dictionary<int, (string, int)>();
            foreach (var ingredient in dish.DishIngredients)
            {
                string ingredientName = string.Empty;
                foreach (var ingredient in source.Ingredients)
                {
                    if (pc.Key == ingredient.Id)
                    {
                        ingredientName = ingredient.IngredientName;
                        break;
                    }
                }
                dishIngredients.Add(pc.Key, (ingredientName, pc.Value));
            }
            return new DishViewModel
            {
                Id = h.Id,
                DishName = dish.DishName,
                Price = dish.Price,
                DishIngredients = dishIngredients
            };
        }
    }
}
