using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Reflection.PortableExecutable;
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

            string xmlUsers = File.ReadAllText(@"../../../Datasets/users.xml");
            Console.WriteLine(ImportUsers);
        }
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(User[]), new XmlRootAttribute("Users"));

            var users = (User[])serializer.Deserialize(new StringReader(inputXml));

            return $"Successfully imported {users.Count()}";
        }
    }
}