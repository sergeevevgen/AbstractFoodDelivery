using System.ComponentModel;

namespace AbstractFoodDeliveryContracts.ViewModels
{
    /// <summary>
    /// Ингредиент, требуемый для изготовления блюда
    /// </summary>
    public class IngredientViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название ингредиента")]
        public string IngredientName { get; set; }
    }
}
