using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.BusinessLogicsContracts;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryContracts.Enums;

namespace AbstractFoodDeliveryBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly IWareHouseStorage _warehouseStorage;
        private readonly IDishStorage _dishStorage;

        public OrderLogic(IOrderStorage orderStorage, IWareHouseStorage wareHouseStorage, IDishStorage dishStorage)
        {
            _orderStorage = orderStorage;
            _warehouseStorage = wareHouseStorage;
            _dishStorage = dishStorage;
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                DishId = model.DishId,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят,
                DateCreate = DateTime.Now
            });
        }

        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (order.Status != Enum.GetName(typeof(OrderStatus), 2))
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                DishId = order.DishId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Выдан
            });
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (order.Status != Enum.GetName(typeof(OrderStatus), 1))
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                DishId = order.DishId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (order.Status != Enum.GetName(typeof(OrderStatus), 0))
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }

            var ingredients = _dishStorage.GetElement(new DishBindingModel { Id = order.DishId });
            bool flag;

            foreach (var ingredient in ingredients.DishIngredients)
            {

                flag = _warehouseStorage.TakeIngredientsInWork(ingredient.Key, ingredient.Value.Item2 * order.Count);
                
                if (!flag)
                    throw new Exception("Недостаточно ингредиентов на складе");
            }

            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                DishId = order.DishId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Выполняется
            });
        }
    }
}
