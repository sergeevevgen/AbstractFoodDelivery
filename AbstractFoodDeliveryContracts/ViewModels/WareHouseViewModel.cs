using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AbstractFoodDeliveryContracts.ViewModels
{
    /// <summary>
    /// Склад для хранения ингредиентов
    /// </summary>
    public class WareHouseViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string WareHouseName { get; set; }
        [DisplayName("ФИО кладовщика")]
        public string StorekeeperFIO { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> WareHouseIngredients { get; set; }
    }
}
