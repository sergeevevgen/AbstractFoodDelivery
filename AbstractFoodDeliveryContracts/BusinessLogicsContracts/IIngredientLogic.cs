using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryContracts.BusinessLogicsContracts
{
    public interface IIngredientLogic
    {
        List<IngredientViewModel> Read(IngredientBindingModel model);

        void CreateOrUpdate(IngredientBindingModel model);

        void Delete(IngredientBindingModel model);
    }
}
