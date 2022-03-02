namespace AbstractFoodDeliveryContracts.BindingModels
{
    /// <summary>
    /// Ингредиент, требуемый для изготовления блюда
    /// </summary>
    public class IngredientBindingModel
    {
        public int? Id { get; set; }
        public string IngredientName { get; set; }
    }
}
