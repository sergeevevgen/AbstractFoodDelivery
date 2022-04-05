using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstractFoodDeliveryDatabaseImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        public void Delete(ClientBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            Client client = context.Clients.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (client != null)
            {
                context.Clients.Remove(client);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractFoodDeliveryDatabase();
            var client = context.Clients
            .Include(rec => rec.Orders)
            .FirstOrDefault(rec => rec.Email == model.Email ||
            rec.Id == model.Id);
            return client != null ? CreateModel(client) : null;
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractFoodDeliveryDatabase();
            return context.Clients
            .Include(rec => rec.Orders)
            .Where(rec => rec.Email.Contains(model.Email))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<ClientViewModel> GetFullList()
        {
            using var context = new AbstractFoodDeliveryDatabase();
            return context.Clients
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(ClientBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Clients.Add(CreateModel(model, new Client()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(ClientBindingModel model)
        {
            using var context = new AbstractFoodDeliveryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Clients.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
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
                ClientFIO = client.ClientFIO,
                Email = client.Email,
                Password = client.Password,
            };
        }
    }
}
