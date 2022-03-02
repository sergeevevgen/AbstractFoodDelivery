using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryContracts.StoragesContracts
{
    public interface IIngredientStorage
    {
        List<IngredientViewModel> GetFullList();
        List<IngredientViewModel> GetFilteredList(IngredientBindingModel model);

        IngredientViewModel GetElement(IngredientBindingModel model);

        void Insert(IngredientBindingModel model);

        void Update(IngredientBindingModel model);

        void Delete(IngredientBindingModel model);
    }
}
