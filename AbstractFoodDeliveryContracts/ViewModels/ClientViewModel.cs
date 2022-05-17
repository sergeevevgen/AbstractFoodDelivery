using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using AbstractFoodDeliveryContracts.Attributes;
using System;
using System.Runtime.Serialization;

namespace AbstractFoodDeliveryContracts.ViewModels
{
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }

        [Column(title: "ФИО", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { get; set; }

        [Column(title: "Логин", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Email { get; set; }

        [Column(title: "Пароль", width: 100)]
        public string Password { get; set; }
    }
}
