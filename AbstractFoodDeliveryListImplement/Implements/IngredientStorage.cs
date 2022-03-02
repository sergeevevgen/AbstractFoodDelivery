using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryListImplement.Models;

namespace AbstractFoodDeliveryListImplement.Implements
{
    public class IngredientStorage : IIngredientStorage
    {
        private readonly DataListSingleton source;
        public IngredientStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public void Delete(IngredientBindingModel model)
        {
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].Id == model.Id.Value)
                {
                    source.Ingredients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public IngredientViewModel GetElement(IngredientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            foreach (var component in source.Ingredients)
            {
                if (component.Id == model.Id || component.IngredientName ==
                    model.IngredientName)
                {
                    return CreateModel(component);
                }
            }
            return null;
        }

        public List<IngredientViewModel> GetFilteredList(IngredientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var result = new List<IngredientViewModel>();
            foreach (var component in source.Ingredients)
            {
                if (component.IngredientName.Contains(model.IngredientName))
                {
                    result.Add(CreateModel(component));
                }
            }
            return result;
        }

        public List<IngredientViewModel> GetFullList()
        {
            var result = new List<IngredientViewModel>();
            foreach (var component in source.Ingredients)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }

        public void Insert(IngredientBindingModel model)
        {
            var tempComponent = new Ingredient { Id = 1 };
            foreach (var component in source.Ingredients)
            {
                if (component.Id >= tempComponent.Id)
                {
                    tempComponent.Id = component.Id + 1;
                }
            }
            source.Ingredients.Add(CreateModel(model, tempComponent));
        }

        public void Update(IngredientBindingModel model)
        {
            Ingredient tempComponent = null;
            foreach (var component in source.Ingredients)
            {
                if (component.Id == model.Id)
                {
                    tempComponent = component;
                }
            }
            if (tempComponent == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempComponent);
        }

        private static Ingredient CreateModel(IngredientBindingModel model, Ingredient component)
        {
            component.IngredientName = model.IngredientName;
            return component;
        }

        private static IngredientViewModel CreateModel(Ingredient component)
        {
            return new IngredientViewModel
            {
                Id = component.Id,
                IngredientName = component.IngredientName
            };
        }
    }
}
