using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AbstractFoodDeliveryContracts.ViewModels
{
    public class ImplementerViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО повара")]
        public string FIO { get; set; }
        [DisplayName("Время перерыва")]
        public int PauseTime { get; set; }
        [DisplayName("Время работы")]
        public int WorkingTime { get; set; }
    }
}
