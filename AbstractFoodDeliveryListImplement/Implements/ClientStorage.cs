using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryListImplement.Models;

namespace AbstractFoodDeliveryListImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        private readonly DataListSingleton source;

        public ClientStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public void Delete(ClientBindingModel model)
        {
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == model.Id.Value)
                {
                    source.Clients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            foreach (var client in source.Clients)
            {
                if (client.Id == model.Id || client.Email ==
                    model.Email)
                {
                    return CreateModel(client);
                }
            }
            return null;
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var result = new List<ClientViewModel>();
            foreach (var client in source.Clients)
            {
                if (client.Email.Contains(model.Email))
                {
                    result.Add(CreateModel(client));
                }
            }
            return result;
        }

        public List<ClientViewModel> GetFullList()
        {
            var result = new List<ClientViewModel>();
            foreach (var client in source.Clients)
            {
                result.Add(CreateModel(client));
            }
            return result;
        }

        public void Insert(ClientBindingModel model)
        {
            var tempClient = new Client { Id = 1 };
            foreach (var client in source.Clients)
            {
                if (client.Id >= tempClient.Id)
                {
                    tempClient.Id = client.Id + 1;
                }
            }
            source.Clients.Add(CreateModel(model, tempClient));
        }

        public void Update(ClientBindingModel model)
        {
            Client tempClient = null;
            foreach (var client in source.Clients)
            {
                if (client.Id == model.Id)
                {
                    tempClient = client;
                }
            }
            if (tempClient == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempClient);
        }

        private static Client CreateModel(ClientBindingModel model, Client client)
        {
            client.Email = model.Email;
            client.ClientFIO = model.ClientFIO;
            client.Password = model.Password;
            return client;
        }

        private static ClientViewModel CreateModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                Email = client.Email,
                ClientFIO = client.ClientFIO,
                Password = client.Password
            };
        }
    }
}
