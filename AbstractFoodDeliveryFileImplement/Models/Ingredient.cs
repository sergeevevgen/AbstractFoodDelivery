using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFoodDeliveryFileImplement.Models
{
    /// <summary>
    /// Компонент, требуемый для изготовления еды
    /// </summary>
    public class Ingredient
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
    }
}
