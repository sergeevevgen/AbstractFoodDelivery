using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFoodDeliveryContracts.ViewModels
{
    public class ReportOrdersByDateViewModel
    {
        public DateTime DateCreate { get; set; }
        public int CountOrders { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
