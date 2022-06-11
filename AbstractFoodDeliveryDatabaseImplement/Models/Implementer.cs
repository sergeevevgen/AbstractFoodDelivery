using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFoodDeliveryDatabaseImplement.Models
{
    public class Implementer
    {
        public int Id { get; set; }
        [Required]
        public string FIO { get; set; }
        [Required]
        public int PauseTime { get; set; }
        [Required]
        public int WorkingTime { get; set; }
        
        /// <summary>
        /// Внешний ключ (связь один ко многим)
        /// </summary>
        [ForeignKey("ImplementerId")]
        public virtual List<Order> Orders { get; set; }
    }
}
