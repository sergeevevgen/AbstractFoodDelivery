using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryDatabaseImplement.Models;

namespace AbstractFoodDeliveryDatabaseImplement.Implements
{
    public class IngredientStorage : IIngredientStorage
    {
        public void Delete(IngredientBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            Ingredient element = context.Ingredients.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.Ingredients.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public IngredientViewModel GetElement(IngredientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractFoodDeliveryDatabase();
            var ingredient = context.Ingredients
            .FirstOrDefault(rec => rec.IngredientName == model.IngredientName || rec.Id
            == model.Id);
            return ingredient != null ? CreateModel(ingredient) : null;
        }

        public List<IngredientViewModel> GetFilteredList(IngredientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractFoodDeliveryDatabase();
            return context.Ingredients
            .Where(rec => rec.IngredientName.Contains(model.IngredientName))
            .Select(CreateModel)
            .ToList();
        }

        public List<IngredientViewModel> GetFullList()
        {
            using var context = new AbstractFoodDeliveryDatabase();
            return context.Ingredients
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(IngredientBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            context.Ingredients.Add(CreateModel(model, new Ingredient()));
            context.SaveChanges();
        }

        public void Update(IngredientBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            var element = context.Ingredients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        private static Ingredient CreateModel(IngredientBindingModel model, Ingredient
        ingredient)
        {
            ingredient.IngredientName = model.IngredientName;
            return ingredient;
        }
        private static IngredientViewModel CreateModel(Ingredient ingredient)
        {
            return new IngredientViewModel
            {
                Id = ingredient.Id,
                IngredientName = ingredient.IngredientName
            };
        }
    }
}
