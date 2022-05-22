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
            View(APIClient.GetRequest<List<WareHouseViewModel>>($"api/WareHouse/GetWareHouseList"));
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
        public void Create(string wareHouseName, string storeKeeperFIO)
        {
            if (string.IsNullOrEmpty(wareHouseName) || string.IsNullOrEmpty(storeKeeperFIO))
            {
                return;
            }
            APIClient.PostRequest("api/WareHouse/CreateOrUpdateWareHouse", new WareHouseBindingModel
            {
                WareHouseName = wareHouseName,
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
            ViewBag.WareHouses = APIClient.GetRequest<List<WareHouseViewModel>>("api/WareHouse/GetWareHouseList");
            ViewBag.Ingredients = APIClient.GetRequest<List<IngredientViewModel>>("api/WareHouse/GetIngredientsList");
            return View();
        }

        [HttpPost]
        public void AddIngredient(int wareHouseId, int ingredientId, int count)
        {
            APIClient.PostRequest("api/WareHouse/AddIngredientToWareHouse", new AddIngredientBindingModel
            {
                WareHouseId = wareHouseId,
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
            ViewBag.WareHouses = APIClient.GetRequest<List<WareHouseViewModel>>("api/WareHouse/GetWareHouseList");
            return View();
        }

        [HttpPost]
        public void Delete(int wareHouseId)
        {
            APIClient.PostRequest("api/WareHouse/DeleteWareHouse", new WareHouseBindingModel 
            { 
                Id = wareHouseId,
                WareHouseName = string.Empty,
                StorekeeperFIO = string.Empty,
                WareHouseIngredients = new Dictionary<int, (string, int)> { }
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Edit(int wareHouseId)
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            WareHouseViewModel wareHouse = APIClient.GetRequest<WareHouseViewModel>($"api/WareHouse/GetWareHouse?wareHouseId={wareHouseId}");
            ViewBag.WareHouseName = wareHouse.WareHouseName;
            ViewBag.StoreKeeperFIO = wareHouse.StorekeeperFIO;
            ViewBag.WareHouseIngredients = wareHouse.WareHouseIngredients.Values;
            return View();
        }

        [HttpPost]
        public void Edit(int wareHouseId, string wareHouseName, string storeKeeperFIO)
        {
            if (string.IsNullOrEmpty(wareHouseName) || string.IsNullOrEmpty(storeKeeperFIO))
            {
                return;
            }
            WareHouseViewModel wareHouse = APIClient.GetRequest<WareHouseViewModel>($"api/WareHouse/GetWareHouse?wareHouseId={wareHouseId}");
            APIClient.PostRequest("api/WareHouse/CreateOrUpdateWareHouse", new WareHouseBindingModel
            {
                Id = wareHouseId,
                WareHouseName = wareHouseName,
                StorekeeperFIO = storeKeeperFIO,
                WareHouseIngredients = wareHouse.WareHouseIngredients,
                DateCreate = DateTime.Now
            });
            Response.Redirect("Index");
        }
    }
}