using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryContracts.StoragesContracts
{
    public interface IWareHouseStorage
    {
        List<WareHouseViewModel> GetFullList();
        List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model);

        WareHouseViewModel GetElement(WareHouseBindingModel model);

        void Insert(WareHouseBindingModel model);

        void Update(WareHouseBindingModel model);

        void Delete(WareHouseBindingModel model);

        bool TakeIngredientsInWork(int dishid, int count);
    }
}
