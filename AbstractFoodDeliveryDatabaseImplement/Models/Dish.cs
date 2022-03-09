using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFoodDeliveryDatabaseImplement.Models
{
    public class Dish
    {
        public int Id { get; set; }
        [Required]
        public string DishName { get; set; }
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("DishId")]
        public virtual List<Order> Orders { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("DishId")]
        public virtual List<DishIngredient> DishIngredients { get; set; }
    }
}
