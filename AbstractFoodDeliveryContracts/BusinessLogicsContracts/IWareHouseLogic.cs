using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryContracts.BusinessLogicsContracts
{
    public interface IWareHouseLogic
    {
        List<WareHouseViewModel> Read(WareHouseBindingModel model);

        void CreateOrUpdate(WareHouseBindingModel model);

        void Delete(WareHouseBindingModel model);

        public void AddIngredient(WareHouseBindingModel model, int ingredientId, int count);
    }
}
