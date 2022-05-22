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
        public List<WareHouseViewModel> GetWareHouseList() => _wareHouseLogic.Read(null)?.ToList();

        [HttpGet]
        public WareHouseViewModel GetWareHouse(int warehouseId) => _wareHouseLogic.Read(new WareHouseBindingModel { Id = warehouseId })?[0];

        [HttpGet]
        public List<IngredientViewModel> GetIngredientsList() => _ingredientLogic.Read(null)?.ToList();

        [HttpPost]
        public void CreateOrUpdateWareHouse(WareHouseBindingModel model) => _wareHouseLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteWareHouse(WareHouseBindingModel model) => _wareHouseLogic.Delete(model);

        [HttpPost]
        public void AddIngredientToWareHouse(AddIngredientBindingModel model) =>
            _wareHouseLogic.AddIngredient(new WareHouseBindingModel
            {
                Id = model.WareHouseId,
            }, model.IngredientId, model.Count);
    }
}