namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            string command = Console.ReadLine();

            string result = GetBooksByAgeRestriction(db, command);
            /*
             Read input from the console in your main method 
             and call your method with the necessary arguments.
             Print the returned string to the console.
             Ignore the casing of the input.    
             */

            Console.WriteLine(result);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder sb = new StringBuilder();

            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);
            var bookTitles = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            foreach (var bookTitle in bookTitles)
            {
                sb.AppendLine(bookTitle);
            }

            return sb.ToString().TrimEnd();
        }
    }
}


