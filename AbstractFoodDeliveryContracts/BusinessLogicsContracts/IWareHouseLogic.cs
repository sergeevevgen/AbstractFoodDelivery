using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryContracts.BusinessLogicsContracts
{
    public interface IWareHouseLogic
    {
        List<WareHouseViewModel> Read(WareHouseBindingModel model);

        void CreateOrUpdate(WareHouseBindingModel model);

        void Delete(WareHouseBindingModel model);
    }
}
