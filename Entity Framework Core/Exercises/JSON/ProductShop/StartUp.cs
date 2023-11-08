using AutoMapper.Execution;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductShop.Data;
using ProductShop.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Text.Json;
using System.Xml.Linq;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new ProductShopContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            ImportData(db);

            Console.WriteLine(GetUsersWithProducts(db));
        }

        public static void ImportData(ProductShopContext context)
        {
            string inputJsonUsers = File.ReadAllText(@"../../../Datasets/users.json");
            ImportUsers(context, inputJsonUsers);

            string inputJsonProducts = File.ReadAllText(@"../../../Datasets/products.json");
            ImportProducts(context, inputJsonProducts);

            string inputJSONCategories = File.ReadAllText($"../../../Datasets/categories.json");
            ImportCategories(context, inputJSONCategories);

            string inputJSONCategoriesProducts = File.ReadAllText(@"../../../Datasets/categories-products.json");
            ImportCategoryProducts(context, inputJSONCategoriesProducts);
        }
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(c => c.Name != null);

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count()}";
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count()}";
        }
        public static string GetUsersWithProducts(ProductShopContext context)
        {           
            var users = context
                 .Users
                 .Where(u => u.ProductsSold.Count >= 1 && u.ProductsSold.Any(p => p.Buyer != null))
                 .OrderByDescending(u => u.ProductsSold
                                          .Where(p => p.Buyer != null)
                                          .Count())
                 .Select(u => new
                 {
                     firstName = u.FirstName,
                     lastName = u.LastName,
                     age = u.Age,
                     soldProducts = new
                     {
                         count = u.ProductsSold
                             .Where(p => p.Buyer != null)
                             .Count(),
                         products = u.ProductsSold
                             .Where(p => p.Buyer != null)
                             .Select(p => new
                             {
                                 name = p.Name,
                                 price = p.Price
                             })
                     }
                 })
                 .ToArray();

            var usersInfo = new
            {
                usersCount = users.Count(),
                users = users
            };

            string usersToJson = JsonConvert.SerializeObject(usersInfo, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            });

            return usersToJson;
        }

    }
}