using System.ComponentModel;

namespace AbstractFoodDeliveryContracts.ViewModels
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int DishId { get; set; }
        public int? ImplementerId { get; set; }

        [DisplayName("Клиент")]
        public string ClientFIO { get; set; }

        [DisplayName("Повар")]
        public string? ImplementerFIO { get; set; }

        [DisplayName("Блюдо")]
        public string DishName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
