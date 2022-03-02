namespace AbstractFoodDeliveryContracts.BindingModels
{
    /// <summary>
    /// Блюда, изготавливаемые в службе
    /// </summary>
    public class DishBindingModel
    {
        public int? Id { get; set; }
        public string DishName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> DishIngredients { get; set; }
    }
}
