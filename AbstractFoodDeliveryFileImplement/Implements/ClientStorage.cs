using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryFileImplement.Models;

namespace AbstractFoodDeliveryFileImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        private readonly FileDataListSingleton source;

        public ClientStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void Delete(ClientBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<ClientViewModel> GetFullList()
        {
            return source.
        }

        public void Insert(ClientBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(ClientBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
