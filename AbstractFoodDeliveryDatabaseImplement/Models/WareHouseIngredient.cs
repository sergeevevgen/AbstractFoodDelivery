using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFoodDeliveryDatabaseImplement.Models
{
    public class WareHouseIngredient
    {
        public int Id { get; set; }
        public int WareHouseId { get; set; }
        public int IngredientId { get; set; }

        [Required]
        public int Count { get; set; }
        public virtual WareHouse WareHouse { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
