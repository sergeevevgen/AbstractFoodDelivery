namespace AbstractFoodDeliveryFileImplement.Models
{
    /// <summary>
    /// Изделие, изготавливаемое в службе
    /// </summary>
    public class Dish
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> DishIngredients { get; set; }
    }
}
