﻿using AbstractFoodDeliveryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstractFoodDeliveryDatabaseImplement
{
    public class AbstractFoodDeliveryDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-FQA7MOU\SQLEXPRESS;Initial Catalog=AbstractFoodDeliveryDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Ingredient> Ingredients { set; get; }
        public virtual DbSet<Dish> Dishes { set; get; }
        public virtual DbSet<DishIngredient> DishIngredients { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }
}
