
namespace AbstractFoodDeliveryContracts.BindingModels
{
    public class WareHouseBindingModel
    {
        public int? Id { get; set; }
        public string WareHouseName { get; set; }
        public string StorekeeperFIO { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, int> WareHouseIngredients { get; set; }
    }
}
