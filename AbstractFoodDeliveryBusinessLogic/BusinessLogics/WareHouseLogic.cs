using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.BusinessLogicsContracts;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryBusinessLogic.BusinessLogics
{
    public class WareHouseLogic : IWareHouseLogic
    {
        private readonly IWareHouseStorage _wareHouseStorage;

        public WareHouseLogic(IWareHouseStorage wareHouseStorage)
        {
            _wareHouseStorage = wareHouseStorage;
        }

        public void CreateOrUpdate(WareHouseBindingModel model)
        {
            var element = _wareHouseStorage.GetElement(new WareHouseBindingModel
            {
                WareHouseName = model.WareHouseName,
                StorekeeperFIO = model.StorekeeperFIO,
                DateCreate = model.DateCreate,
                WareHouseIngredients = model.WareHouseIngredients
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }

            if (model.Id.HasValue)
            {
                _wareHouseStorage.Update(model);
            }
            else
            {
                _wareHouseStorage.Insert(model);
            }
        }

        public void Delete(WareHouseBindingModel model)
        {
            var element = _wareHouseStorage.GetElement(new WareHouseBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _wareHouseStorage.Delete(model);
        }

        public List<WareHouseViewModel> Read(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return _wareHouseStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<WareHouseViewModel>() { _wareHouseStorage.GetElement(model) };
            }

            return _wareHouseStorage.GetFilteredList(model);
        }
    }
}
