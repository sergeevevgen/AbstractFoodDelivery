using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryContracts.BusinessLogicsContracts
{
    public interface IDishLogic
    {
        List<DishViewModel> Read(DishBindingModel model);

        void CreateOrUpdate(DishBindingModel model);

        void Delete(DishBindingModel model);
    }
}
