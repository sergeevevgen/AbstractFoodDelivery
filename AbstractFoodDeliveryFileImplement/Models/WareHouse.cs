
namespace AbstractFoodDeliveryFileImplement.Models
{
    /// <summary>
    /// Склад
    /// </summary>
    public class WareHouse
    {
        public int Id { get; set; }
        public string WareHouseName { get; set; }
        public string StorekeeperFIO { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, int> WareHouseIngredients { get; set; }
    }
}
