using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFoodDeliveryDatabaseImplement.Models
{
    public class WareHouse
    {
        public int Id { get; set; }
        [Required]
        public string WareHouseName { get; set; }
        [Required]
        public string StorekeeperFIO { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("WareHouseId")]
        public virtual List<WareHouseIngredient> WareHouseIngredients { get; set; }
    }
}
