using System.ComponentModel.DataAnnotations;

namespace AbstractFoodDeliveryDatabaseImplement.Models
{
    /// <summary>
    /// Сколько ингредиентов требуется при изготовлении блюда
    /// </summary>
    public class DishIngredient
    {
        public int Id { get; set; } 
        public int DishId { get; set; }
        public int IngredientId { get; set; }

        [Required]
        public int Count { get; set; }
        public virtual Dish Dish { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
