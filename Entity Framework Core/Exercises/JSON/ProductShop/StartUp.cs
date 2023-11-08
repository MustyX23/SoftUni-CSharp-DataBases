using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System.Text.Json;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new ProductShopContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            string inputJSON = File.ReadAllText(@"../../../Datasets/users.json");
            Console.WriteLine(ImportUsers(db, inputJSON));
        }
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }
    }
}