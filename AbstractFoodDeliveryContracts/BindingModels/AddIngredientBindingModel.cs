using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFoodDeliveryContracts.BindingModels
{
    public class AddIngredientBindingModel
    {
        public int WareHouseId { get; set; }
        public int IngredientId { get; set; }
        public int Count { get; set; }
    }
}
