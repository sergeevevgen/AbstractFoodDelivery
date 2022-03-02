namespace AbstractFoodDeliveryContracts.BindingModels
{
    /// <summary>
    /// Данные от клиента для создания заказа
    /// </summary>
    public class CreateOrderBindingModel
    {
        public int DishId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
