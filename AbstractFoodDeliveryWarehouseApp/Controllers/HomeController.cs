using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AbstractFoodDeliveryWarehouseApp.Models;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.ViewModels;
using Microsoft.Extensions.Configuration;

namespace AbstractFoodDeliveryWarehouseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            return
            View(APIClient.GetRequest<List<WareHouseViewModel>>($"api/Warehouse/GetWarehouseList"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (_configuration["Password"] != password)
                {
                    throw new Exception("Неверный пароль");
                }
                Program.Autorized = true;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите пароль");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public void Create(string warehouseName, string storeKeeperFIO)
        {
            if (string.IsNullOrEmpty(warehouseName) || string.IsNullOrEmpty(storeKeeperFIO))
            {
                return;
            }
            APIClient.PostRequest("api/Warehouse/CreateOrUpdateWarehouse", new WareHouseBindingModel
            {
                WareHouseName = warehouseName,
                StorekeeperFIO = storeKeeperFIO,
                DateCreate = DateTime.Now,
                WareHouseIngredients = new Dictionary<int, (string, int)>()
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult AddIngredient()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Warehouses = APIClient.GetRequest<List<WareHouseViewModel>>("api/Warehouse/GetWarehouseList");
            ViewBag.Conditions = APIClient.GetRequest<List<IngredientViewModel>>("api/Warehouse/GetIngredientsList");
            return View();
        }

        [HttpPost]
        public void AddIngredient(int warehouseId, int ingredientId, int count)
        {
            APIClient.PostRequest("api/Warehouse/AddIngredientToWarehouse", new AddIngredientBindingModel
            {
                WarehouseId = warehouseId,
                IngredientId = ingredientId,
                Count = count
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Warehouses = APIClient.GetRequest<List<WareHouseViewModel>>("api/Warehouse/GetWarehouseList");
            return View();
        }

        [HttpPost]
        public void Delete(int warehouseId)
        {
            APIClient.PostRequest("api/Warehouse/DeleteWarehouse", new WareHouseBindingModel
            {
                Id = warehouseId
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Privacy(int warehouseId)
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            WareHouseViewModel warehouse = APIClient.GetRequest<WareHouseViewModel>($"api/Warehouse/GetWarehouse?warehouseId={warehouseId}");
            ViewBag.WareHouseName = warehouse.WareHouseName;
            ViewBag.StoreKeeperFIO = warehouse.StorekeeperFIO;
            ViewBag.WarehouseIngredients = warehouse.WareHouseIngredients.Values;
            return View();
        }

        [HttpPost]
        public void Privacy(int warehouseId, string warehouseName, string storeKeeperFIO)
        {
            if (string.IsNullOrEmpty(warehouseName) || string.IsNullOrEmpty(storeKeeperFIO))
            {
                return;
            }
            WareHouseViewModel warehouse = APIClient.GetRequest<WareHouseViewModel>($"api/Warehouse/GetWarehouse?warehouseId={warehouseId}");
            APIClient.PostRequest("api/Warehouse/CreateOrUpdateWarehouse", new WareHouseBindingModel
            {
                Id = warehouseId,
                WareHouseName = warehouseName,
                StorekeeperFIO = storeKeeperFIO,
                WareHouseIngredients = warehouse.WareHouseIngredients,
                DateCreate = DateTime.Now
            });
            Response.Redirect("Index");
        }
    }
}