using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<DishViewModel> Dishes { get; set; }
    }
}
