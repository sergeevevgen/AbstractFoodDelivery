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
        /// <summary>
        /// Объект-заглушка
        /// </summary>
        private readonly object _locker = new object();
        public OrderLogic(IOrderStorage orderStorage, IWareHouseStorage wareHouseStorage)
        {
            _orderStorage = orderStorage;
            _warehouseStorage = wareHouseStorage;
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                DishId = model.DishId,
                ClientId = model.ClientId,
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
                ClientId = order.ClientId,
                DishId = order.DishId,
                ImplementerId = order.ImplementerId,
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
            if (order.Status == Enum.GetName(typeof(OrderStatus), 4))
            {
                return;
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                DishId = order.DishId,
                ImplementerId = order.ImplementerId,
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
            lock (_locker)
            {
                var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
                if (order == null)
                {
                    throw new Exception("Заказ не найден");
                }
                if (order.Status != Enum.GetName(typeof(OrderStatus), 0) && order.Status != Enum.GetName(typeof(OrderStatus), 4))
                {
                    throw new Exception("Заказ не в статусе \"Принят\" или \"Требуются материалы\"");
                }
                if (!_warehouseStorage.TakeIngredientsInWork(order.DishId, order.Count))
                {
                    _orderStorage.Update(new OrderBindingModel
                    {
                        Id = order.Id,
                        ClientId = order.ClientId,
                        ImplementerId = model.ImplementerId,
                        DishId = order.DishId,
                        Count = order.Count,
                        Sum = order.Sum,
                        DateCreate = order.DateCreate,
                        Status = OrderStatus.Требуются_Материалы
                    });
                }
                else
                {
                    _orderStorage.Update(new OrderBindingModel
                    {
                        Id = order.Id,
                        ClientId = order.ClientId,
                        ImplementerId = model.ImplementerId,
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
    }
}
