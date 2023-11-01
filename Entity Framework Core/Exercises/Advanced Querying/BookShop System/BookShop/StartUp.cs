namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Text;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            string input = Console.ReadLine();

            string result = GetBooksByCategory(db, input);

            Console.WriteLine(result);
        }
        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var goldenEdition = Enum.Parse<EditionType>("Gold", true);

            var books = context.Books
                .Where(b => b.EditionType == goldenEdition && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .ToArray();

            /*
             * Return in a single string the titles of 
             * the golden edition books that have less than 5000 copies
             * , each on a new line. Order them by BookId ascending.
             */

            foreach (var bookTitle in books)
            {
                sb.AppendLine(bookTitle.Title);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    Title = b.Title,
                    Price = b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToArray();

            /*
             Return in a single string all titles 
             and prices of books with a price higher than 40, each 
             on a new row in the format given below. 
             Order them by price descending.
             */

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .ToArray();
            /*
             *Return in a single string with all
             *titles of books that are NOT released in a given year.
             *Order them by bookId ascending.
             */

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            string[] categories = input.ToLower().Split(' ');

            var bookCategories = context.BooksCategories
                .Where(bc => categories.Contains(bc.Category.Name))
                .Select(bc => new { BookTitle = bc.Book.Title })
                .OrderBy(b => b.BookTitle)
                .ToArray();



            /*
              Return in a single string the titles of books by a given list of categories.
              The list of categories will be given in a single line separated by one or more spaces. 
              Ignore casing. 
              Order by title alphabetically.
             
             */

            foreach (var book in bookCategories)
            {
                sb.AppendLine(book.BookTitle);
            }

            return sb.ToString().TrimEnd();
        }
    }
}


