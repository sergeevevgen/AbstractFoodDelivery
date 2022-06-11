using System.ComponentModel;
using AbstractFoodDeliveryContracts.Attributes;
using System;
using System.Runtime.Serialization;

namespace AbstractFoodDeliveryContracts.ViewModels
{
    /// <summary>
    /// Блюдо, изготавливаемое в службе
    /// </summary>
    public class DishViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        [Column(title: "Название блюда", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string DishName { get; set; }

        [Column(title: "Цена", width: 100)]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> DishIngredients { get; set; }
    }
}
