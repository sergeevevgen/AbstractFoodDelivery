using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.BusinessLogicsContracts;
using AbstractFoodDeliveryContracts.ViewModels;

namespace TravelCompanyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWareHouseLogic _wareHouseLogic;
        private readonly IIngredientLogic _ingredientLogic;

        public WarehouseController(IWareHouseLogic wareHouseLogic, IIngredientLogic ingredientLogic)
        {
            _wareHouseLogic = wareHouseLogic;
            _ingredientLogic = ingredientLogic;
        }

        [HttpGet]
        public List<WareHouseViewModel> GetWarehouseList() => _wareHouseLogic.Read(null)?.ToList();

        [HttpGet]
        public WareHouseViewModel GetWarehouse(int warehouseId) => _wareHouseLogic.Read(new WareHouseBindingModel { Id = warehouseId })?[0];

        [HttpGet]
        public List<IngredientViewModel> GetIngredientsList() => _ingredientLogic.Read(null)?.ToList();

        [HttpPost]
        public void CreateOrUpdateWarehouse(WareHouseBindingModel model) => _wareHouseLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteWarehouse(WareHouseBindingModel model) => _wareHouseLogic.Delete(model);

        [HttpPost]
        public void AddIngredientToWarehouse(AddIngredientBindingModel model) =>
            _wareHouseLogic.AddIngredient(new WareHouseBindingModel
            {
                Id = model.WarehouseId,
            }, model.IngredientId, model.Count);
    }
}