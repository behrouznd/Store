﻿using Domain.Entities.Categories;
using Domain.Entities.Products;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext aspnetrunContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
              

                if (!aspnetrunContext.Categories.Any())
                {
                    aspnetrunContext.Categories.AddRange(GetPreconfiguredCategories());
                    await aspnetrunContext.SaveChangesAsync();
                }

                if (!aspnetrunContext.Products.Any())
                {
                    aspnetrunContext.Products.AddRange(GetPreconfiguredProducts());
                    await aspnetrunContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<StoreContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(aspnetrunContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>()
            {
                new Category() { CategoryName = "Phone"},
                new Category() { CategoryName = "TV"}
            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product() { ProductName = "IPhone", CategoryId = 1 , UnitPrice = 19.5M , UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Discontinued = false },
                new Product() { ProductName = "Samsung", CategoryId = 1 , UnitPrice = 33.5M , UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Discontinued = false },
                new Product() { ProductName = "LG TV", CategoryId = 2 , UnitPrice = 33.5M , UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Discontinued = false }
            };
        }
    }
}
