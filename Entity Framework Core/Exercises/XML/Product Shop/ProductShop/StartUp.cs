using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System;
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

            Console.WriteLine(GetProductsInRange(db));
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


    }
}