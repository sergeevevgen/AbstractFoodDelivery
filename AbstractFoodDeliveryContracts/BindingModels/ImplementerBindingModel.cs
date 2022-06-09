using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFoodDeliveryContracts.BindingModels
{
    public class ImplementerBindingModel
    {
        public int? Id { get; set; }
        public string FIO { get; set; }
        public int PauseTime { get; set; }
        public int WorkingTime { get; set; }
    }
}
