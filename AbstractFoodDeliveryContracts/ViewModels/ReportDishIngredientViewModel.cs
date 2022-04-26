using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFoodDeliveryContracts.ViewModels
{
    public class ReportDishIngredientViewModel
    {
        public string DishName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Ingredients { get; set; }
    }
}
