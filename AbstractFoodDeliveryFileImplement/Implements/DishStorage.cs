using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryFileImplement.Models;

namespace AbstractFoodDeliveryFileImplement.Implements
{
    public class DishStorage : IDishStorage
    {
        private readonly FileDataListSingleton source;
        public DishStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<DishViewModel> GetFullList()
        {
            return source.Dishes
            .Select(CreateModel)
            .ToList();
        }
        public List<DishViewModel> GetFilteredList(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Dishes
            .Where(rec => rec.DishName.Contains(model.DishName))
            .Select(CreateModel)
            .ToList();
        }
        public DishViewModel GetElement(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var dish = source.Dishes
            .FirstOrDefault(rec => rec.DishName == model.DishName || rec.Id
           == model.Id);
            return dish != null ? CreateModel(dish) : null;
        }
        public void Insert(DishBindingModel model)
        {
            int maxId = source.Dishes.Count > 0 ? source.Ingredients.Max(rec => rec.Id)
: 0;
            var element = new Dish
            {
                Id = maxId + 1,
                DishIngredients = new Dictionary<int, int>()
            };
            source.Dishes.Add(CreateModel(model, element));
        }
        public void Update(DishBindingModel model)
        {
            var element = source.Dishes.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(DishBindingModel model)
        {
            Dish element = source.Dishes.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Dishes.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
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
            // обновляем существуюущие и добавляем новые
            foreach (var ingredient in model.DishIngredients)
            {
                if (dish.DishIngredients.ContainsKey(ingredient.Key))
                {
                    dish.DishIngredients[ingredient.Key] =
                   model.DishIngredients[ingredient.Key].Item2;
                }
                else
                {
                    dish.DishIngredients.Add(ingredient.Key,
                   model.DishIngredients[ingredient.Key].Item2);
                }
            }
            return dish;
        }
        private DishViewModel CreateModel(Dish dish)
        {
            return new DishViewModel
            {
                Id = dish.Id,
                DishName = dish.DishName,
                Price = dish.Price,
                DishIngredients = dish.DishIngredients
                .ToDictionary(recPC => recPC.Key, recPC =>
                (source.Ingredients.FirstOrDefault(recC => recC.Id ==
                recPC.Key)?.IngredientName, recPC.Value))
            };
        }
    }
}
