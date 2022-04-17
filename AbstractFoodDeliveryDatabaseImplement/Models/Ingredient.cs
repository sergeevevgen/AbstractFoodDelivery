using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFoodDeliveryDatabaseImplement.Models
{
    /// <summary>
    /// Ингредиент, требуемый для изготовления блюда
    /// </summary>
    public class Ingredient
    {
        /// <summary>
        /// Номер
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Обязательно к заполнению
        /// </summary>
        [Required]
        public string IngredientName { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("IngredientId")]
        public virtual List<DishIngredient> DishIngredients { get; set; }

        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("IngredientId")]
        public virtual List<WareHouseIngredient> WareHouseIngredients { get; set; } 
    }
}
