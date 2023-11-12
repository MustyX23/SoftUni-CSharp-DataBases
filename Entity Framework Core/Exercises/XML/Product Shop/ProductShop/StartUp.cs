using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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

        public static void ImportData(ProductShopContext cotext)
        {
            string xmlUsers = File.ReadAllText(@"../../../Datasets/users.xml");
            ImportUsers(cotext, xmlUsers);

            string xmlProducts = File.ReadAllText(@"../../../Datasets/products.xml");
            ImportProducts(cotext, xmlProducts);

            string xmlCategories = File.ReadAllText(@"../../../Datasets/categories.xml");
            ImportCategories(cotext, xmlCategories);

            string xmlCategorieProducts = File.ReadAllText(@"../../../Datasets/categories-products.xml");
            ImportCategoryProducts(cotext, xmlCategorieProducts);
        }
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserDTO[]), new XmlRootAttribute("Users"));

            var usersDtos = (UserDTO[])serializer.Deserialize(new StringReader(inputXml));

            List<User> users = new List<User>();

            foreach (var userDto in usersDtos)
            {
                var user = new User()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Age = userDto.Age
                };
                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProductDTO[]), new XmlRootAttribute("Products"));

            var productsDTOS = (ProductDTO[])serializer.Deserialize(new StringReader(inputXml));

            Product[] products = productsDTOS
                .Select(p => new Product()
                {
                    Name = p.Name,
                    Price = p.Price,
                    SellerId = p.SellerId,
                    BuyerId = p.BuyerId
                })
                .ToArray();

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";
        }
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CategoryDTO[]), new XmlRootAttribute("Categories"));

            var categoriesDTOS = (CategoryDTO[])serializer.Deserialize(new StringReader(inputXml));

            var categories = categoriesDTOS
                .Select(c => new Category()
                {
                    Name = c.Name
                })
                .ToArray();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count()}";
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(CategoryProductDTO[]), new XmlRootAttribute("CategoryProducts"));

            var categoryProductsDTOS = (CategoryProductDTO[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var categoryProducts = categoryProductsDTOS
                .Select(c => new CategoryProduct()
                {
                    CategoryId = c.CategoryId,
                    ProductId = c.ProductId
                })
                .ToList();

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {            
            ProductExportDTO[] products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new ProductExportDTO()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                })
                .Take(10)
                .ToArray();
            /*
             * Get all products in a specified price range
             * between 500 and 1000(inclusive)
             * .Order them by price(from lowest to highest)
             * .Select only the product name,
             * price and the full name of the buyer.Take top 10 records.
             */
            var xmlSerializer = new XmlSerializer(typeof(ProductExportDTO[]), new XmlRootAttribute("Products"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();
            using StringWriter writer = new StringWriter(sb);

            xmlSerializer.Serialize(writer, products, namespaces);

            return sb.ToString().TrimEnd();
        }
        //public static string GetSoldProducts(ProductShopContext context)
        //{
        //    UserExportDTO[] users = context.Users
        //        .Where(u => u.ProductsSold.Count >= 1)
        //        .OrderBy(u => u.LastName)
        //        .ThenBy(u => u.FirstName)
        //        .Select(u => new UserExportDTO()
        //        {
        //            FirstName = u.FirstName,
        //            LastName = u.LastName,
        //            SoldProducts = u.ProductsSold.Select(p => new SoldProductExportDTO()
        //            {
        //                Name = p.Name,
        //                Price = p.Price
        //            })
        //            .ToArray()
        //        })
        //        .Take(5)
        //        .ToArray();

        //    var xmlSerializer = new XmlSerializer(typeof(UserExportDTO[]), new XmlRootAttribute("Users"));

        //    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        //    namespaces.Add(string.Empty, string.Empty);

        //    StringBuilder sb = new StringBuilder();

        //    using StringWriter writer = new StringWriter(sb);

        //    xmlSerializer.Serialize(writer, users, namespaces);

        //    return sb.ToString().TrimEnd();

        //}

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            CategoryExportDTO[] categories = context.Categories
                .Select(c => new CategoryExportDTO()
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(p => p.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(p => p.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToArray();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add (string.Empty, string.Empty);

            XmlSerializer serializer = new XmlSerializer(typeof(CategoryExportDTO[]), new XmlRootAttribute("Categories"));

            StringBuilder sb = new StringBuilder();
            using StringWriter writer = new StringWriter(sb);

            serializer.Serialize(writer, categories, namespaces);

            return sb.ToString().TrimEnd();

        }
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            UserExportDTO[] users = context.Users
                .ToArray()
                .Where(u => u.ProductsSold.Count >= 1)
                .OrderByDescending(u => u.ProductsSold.Count)
                .Select(u => new UserExportDTO()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductContainer() 
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold.Select(ps => new SoldProductExportDTO()
                        {
                            Name = ps.Name,
                            Price = ps.Price,
                        })
                        .OrderByDescending(ps => ps.Price).ToArray()
                    }                   
                })
                .Take(10)
                .ToArray();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();
            using StringWriter writer = new StringWriter(sb);

            XmlSerializer serializer = new XmlSerializer(typeof(UserExportDTO[]), new XmlRootAttribute("Users"));
            serializer.Serialize(writer, users);

            return sb.ToString().TrimEnd();
        }
    }
}