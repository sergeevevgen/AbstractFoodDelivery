using System.ComponentModel;
using AbstractFoodDeliveryContracts.Attributes;
using System;
using System.Runtime.Serialization;

namespace AbstractFoodDeliveryContracts.ViewModels
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class OrderViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int DishId { get; set; }
        public int? ImplementerId { get; set; }

        [Column(title: "Клиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { get; set; }

        [Column(title: "Повар", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ImplementerFIO { get; set; }

        [Column(title: "Блюдо", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string DishName { get; set; }

        [Column(title: "Количество", width: 50)]
        public int Count { get; set; }

        [Column(title: "Сумма", width: 100)]
        public decimal Sum { get; set; }

        [Column(title: "Статус", width: 100)]
        public string Status { get; set; }

        [Column(title: "Дата создания", width: 100)]
        public DateTime DateCreate { get; set; }

        [Column(title: "Дата выполнения", width: 100)]
        public DateTime? DateImplement { get; set; }
    }
}
