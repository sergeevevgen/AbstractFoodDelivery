using Microsoft.AspNetCore.Mvc;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.BusinessLogicsContracts;
using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IDishLogic _dish;
        public MainController(IOrderLogic order, IDishLogic dish)
        {
            _order = order;
            _dish = dish;
        }

        [HttpGet]
        public List<DishViewModel> GetDishList() => _dish
            .Read(null)?.ToList();
        
        [HttpGet]
        public DishViewModel GetDish(int dishId) => _dish
            .Read(new DishBindingModel { Id = dishId })?[0];

        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order
            .Read(new OrderBindingModel { ClientId = clientId });

        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _order
            .CreateOrder(model);
    }
}
