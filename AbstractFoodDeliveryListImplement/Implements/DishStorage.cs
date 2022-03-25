using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryListImplement.Models;

namespace AbstractFoodDeliveryListImplement.Implements
{
    public class DishStorage : IDishStorage
    {
        private readonly DataListSingleton source;
        public DishStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public void Delete(DishBindingModel model)
        {
            for (int i = 0; i < source.Dishes.Count; ++i)
            {
                if (source.Dishes[i].Id == model.Id)
                {
                    source.Dishes.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public DishViewModel GetElement(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var dish in source.Dishes)
            {
                if (dish.Id == model.Id || dish.DishName ==
                model.DishName)
                {
                    return CreateModel(dish);
                }
            }
            return null;
        }

        public List<DishViewModel> GetFilteredList(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<DishViewModel>();
            foreach (var dish in source.Dishes)
            {
                if (dish.DishName.Contains(model.DishName))
                {
                    result.Add(CreateModel(dish));
                }
            }
            return result;
        }

        public List<DishViewModel> GetFullList()
        {
            var result = new List<DishViewModel>();
            foreach (var dish in source.Dishes)
            {
                result.Add(CreateModel(dish));
            }
            return result;
        }

        public void Insert(DishBindingModel model)
        {
            var tempDish = new Dish
            {
                Id = 1,
                DishIngredients = new Dictionary<int, int>()
            };
            foreach (var dish in source.Dishes)
            {
                if (dish.Id >= tempDish.Id)
                {
                    tempDish.Id = dish.Id + 1;
                }
            }
            source.Dishes.Add(CreateModel(model, tempDish));

        }

        public void Update(DishBindingModel model)
        {
            Dish tempDish = null;
            foreach (var dish in source.Dishes)
            {
                if (dish.Id == model.Id)
                {
                    tempDish = dish;
                }
            }

            if (tempDish == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempDish);
        }

        private static Dish CreateModel(DishBindingModel model, Dish dish)
        {
            dish.DishName = model.DishName;
            dish.Price = model.Price;
            // удаляем убранные
            foreach (var key in dish.DishIngredients.Keys.ToList())
            {
                if (!model.DishIngredients.ContainsKey(key))
                {
                    dish.DishIngredients.Remove(key);
                }
            }
            // обновляем существующие и добавляем новые
            foreach (var component in model.DishIngredients)
            {
                if (dish.DishIngredients.ContainsKey(component.Key))
                {
                    dish.DishIngredients[component.Key] =
                    model.DishIngredients[component.Key].Item2;
                }
                else
                {
                    dish.DishIngredients.Add(component.Key,
                    model.DishIngredients[component.Key].Item2);
                }
            }
            return dish;
        }

        private DishViewModel CreateModel(Dish dish)
        {
            // требуется дополнительно получить список компонентов для изделия с
            //названиями и их количество
            var dishIngredients = new Dictionary<int, (string, int)>();
            foreach (var pc in dish.DishIngredients)
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
                Id = dish.Id,
                DishName = dish.DishName,
                Price = dish.Price,
                DishIngredients = dishIngredients
            };
        }
    }
}
