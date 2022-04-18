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
        private readonly string WareHouseFileName = "WareHouse.xml";
        public List<Ingredient> Ingredients { get; set; }
        public List<Order> Orders { get; set; }
        public List<Dish> Dishes { get; set; }
        public List<WareHouse> WareHouses { get; set; }

        private FileDataListSingleton()
        {
            Ingredients = LoadIngredients();
            Orders = LoadOrders();
            Dishes = LoadDishes();
            WareHouses = LoadWareHouses();
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
            instance.SaveWareHouses();
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
            if (File.Exists(OrderFileName))
            {
                var xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    bool dateimplement = elem.Element("DateImplement").IsEmpty;
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DishId = Convert.ToInt32(elem.Element("DishId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = dateimplement ? null : Convert.ToDateTime(elem.Element("DateImplement").Value)
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
        private List<WareHouse> LoadWareHouses()
        {
            var list = new List<WareHouse>();
            if (File.Exists(WareHouseFileName))
            {
                var xDocument = XDocument.Load(WareHouseFileName);
                var xElements = xDocument.Root.Elements("WareHouse").ToList();
                foreach (var elem in xElements)
                {
                    var warehouseIngr = new Dictionary<int, int>();
                    foreach (var ingredient in
                    elem.Element("WareHouseIngredients").Elements("WareHouseIngredient").ToList())
                    {
                        warehouseIngr.Add(Convert.ToInt32(ingredient.Element("Key").Value),
                        Convert.ToInt32(ingredient.Element("Value").Value));
                    }
                    list.Add(new WareHouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        WareHouseName = elem.Element("WareHouseName").Value,
                        StorekeeperFIO = elem.Element("StorekeeperFIO").Value,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        WareHouseIngredients = warehouseIngr
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
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("DishId", order.DishId),
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
        private void SaveWareHouses()
        {
            if (WareHouses != null)
            {
                var xElement = new XElement("WareHouses");
                foreach (var warehouse in WareHouses)
                {
                    var ingrElement = new XElement("WareHouseIngredients");
                    foreach (var ingredient in warehouse.WareHouseIngredients)
                    {
                        ingrElement.Add(new XElement("WareHouseIngredient",
                        new XElement("Key", ingredient.Key),
                        new XElement("Value", ingredient.Value)));
                    }
                    xElement.Add(new XElement("WareHouse",
                     new XAttribute("Id", warehouse.Id),
                     new XElement("WareHouseName", warehouse.WareHouseName),
                     new XElement("StorekeeperFIO", warehouse.StorekeeperFIO),
                     new XElement("DateCreate", warehouse.DateCreate),
                     ingrElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(WareHouseFileName);
            }
        }
    }

}
