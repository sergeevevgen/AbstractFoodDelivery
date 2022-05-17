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
    /// <summary>
    /// Сообщения, приходящие на почту
    /// </summary>
    public class MessageInfoViewModel
    {
        [Column(title: "Номер", width: 50)]
        public string MessageId { get; set; }
        [Column(title: "Отправитель", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string SenderName { get; set; }
        [Column(title: "Дата письма", width: 100)]
        public DateTime DateDelivery { get; set; }
        [Column(title: "Заголовок", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Subject { get; set; }
        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }
    }
}
