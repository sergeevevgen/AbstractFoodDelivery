using AbstractFoodDeliveryContracts.Enums;
using AbstractFoodDeliveryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AbstractFoodDeliveryFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string IngredientFileName = "Ingredient.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string DishFileName = "Dish.xml";
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";
        public List<Ingredient> Ingredients { get; set; }
        public List<Order> Orders { get; set; }
        public List<Dish> Dishes { get; set; }
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        private FileDataListSingleton()
        {
            Ingredients = LoadIngredients();
            Orders = LoadOrders();
            Dishes = LoadDishes();
            Clients = LoadClients();
            Implementers = LoadImplementers();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        public static void SaveData()
        {
            instance.SaveIngredients();
            instance.SaveOrders();
            instance.SaveDishes();
            instance.SaveClients();
            instance.SaveImplementers();
        }
        private List<Ingredient> LoadIngredients()
        {
            var list = new List<Ingredient>();
            if (File.Exists(IngredientFileName))
            {
                var xDocument = XDocument.Load(IngredientFileName);
                var xElements = xDocument.Root.Elements("Ingredient").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Ingredient
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        IngredientName = elem.Element("IngredientName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if(File.Exists(OrderFileName))
            {
                var xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach(var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DishId = Convert.ToInt32(elem.Element("DishId").Value),
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        ImplementerId = elem.Element("ImplementerId").IsEmpty ? null : Convert.ToInt32(elem.Element("ImplementerId").Value) ,
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus) Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = elem.Element("DateImplement").IsEmpty ? null : Convert.ToDateTime(elem.Element("DateImplement").Value)
                    });
                }
            }
            return list;
        }
        private List<Dish> LoadDishes()
        {
            var list = new List<Dish>();
            if (File.Exists(DishFileName))
            {
                var xDocument = XDocument.Load(DishFileName);
                var xElements = xDocument.Root.Elements("Dish").ToList();
                foreach (var elem in xElements)
                {
                    var dishIngr = new Dictionary<int, int>();
                    foreach (var ingredient in
                    elem.Element("DishIngredients").Elements("DishIngredient").ToList())
                    {
                        dishIngr.Add(Convert.ToInt32(ingredient.Element("Key").Value),
                       Convert.ToInt32(ingredient.Element("Value").Value));
                    }
                    list.Add(new Dish
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DishName = elem.Element("DishName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        DishIngredients = dishIngr
                    });
                }
            }
            return list;
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if(File.Exists(ClientFileName))
            {
                var xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach(var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }
            return list;
        }

        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                var xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FIO = elem.Element("FIO").Value,
                        PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value),
                        WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value)
                    });
                }
            }
            return list;
        }

        private void SaveIngredients()
        {
            if (Ingredients != null)
            {
                var xElement = new XElement("Ingredients");
                foreach (var ingredient in Ingredients)
                {
                    xElement.Add(new XElement("Ingredient",
                        new XAttribute("Id", ingredient.Id),
                        new XElement("IngredientName", ingredient.IngredientName)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(IngredientFileName);
            }
        }
        private void SaveOrders()
        {
            if(Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach(var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                        new XAttribute("Id", order.Id),
                        new XElement("DishId", order.DishId),
                        new XElement("ClientId", order.ClientId),
                        new XElement("ImplementerId", order.ImplementerId),
                        new XElement("Count", order.Count),
                        new XElement("Sum", order.Sum),
                        new XElement("Status", order.Status),
                        new XElement("DateCreate", order.DateCreate),
                        new XElement("DateImplement", order.DateImplement)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveDishes()
        {
            if (Dishes != null)
            {
                var xElement = new XElement("Dishes");
                foreach (var dish in Dishes)
                {
                    var ingrElement = new XElement("DishIngredients");
                    foreach (var ingredient in dish.DishIngredients)
                    {
                        ingrElement.Add(new XElement("DishIngredient",
                        new XElement("Key", ingredient.Key),
                        new XElement("Value", ingredient.Value)));
                    }
                    xElement.Add(new XElement("Dish",
                        new XAttribute("Id", dish.Id),
                        new XElement("DishName", dish.DishName),
                        new XElement("Price", dish.Price),
                        ingrElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(DishFileName);
            }
        }

        private void SaveClients()
        {
            if(Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach(var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                        new XAttribute("Id", client.Id),
                        new XElement("ClientFIO", client.ClientFIO),
                        new XElement("Email", client.Email),
                        new XElement("Password", client.Password)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }

        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                        new XAttribute("Id", implementer.Id),
                        new XElement("FIO", implementer.FIO),
                        new XElement("PauseTime", implementer.PauseTime),
                        new XElement("WorkingTime", implementer.WorkingTime)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
    }

}
