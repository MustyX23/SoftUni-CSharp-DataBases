namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Text;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main()
        {
            var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            //string input = Console.ReadLine();

            //string result = GetBooksByAuthor(db, input);

            //Console.WriteLine(result);
            //int lengthCheck = int.Parse(Console.ReadLine());
            //Console.WriteLine(CountBooks(db, lengthCheck));

            //Console.WriteLine(GetMostRecentBooks(db));

            //IncreasePrices(db);

            Console.WriteLine(RemoveBooks(db));


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
                .Select(bc => new { BookTitle = bc.Book.Title.ToLower()})
                .OrderBy(b => b.BookTitle)
                .ToArray();



            /*
              Return in a single string the titles of books by a given list of categories.
              The list of categories will be given in a single line separated by one or more spaces. 
              Ignore casing. 
              Order by title alphabetically.
             
             */
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            foreach (var book in bookCategories)
            {
                sb.AppendLine(textInfo.ToTitleCase(book.BookTitle));
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder sb = new StringBuilder();

            /*
               Return the title, edition type and
               price of all books that are released 
               before a given date. The date will be a string 
               in the format "dd-MM-yyyy".
               Return all of the rows in a single string,
               ordered by release date (descending).
             */

            DateTime convertedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < convertedDate)
                .Select(b => new
                {
                    b.ReleaseDate,
                    b.Price,
                    Title = b.Title,
                    EditionType = b.EditionType,
                })
                .OrderByDescending(b => b.ReleaseDate)
                .ToArray();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            /*
              Return the full names of authors, 
              whose first name ends with a given string.
              Return all names in a single string,
              each on a new row, ordered alphabetically.
             */

            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = $"{a.FirstName} {a.LastName}"
                })
                .OrderBy(a => a.FullName)
                .ToArray();

            foreach (var author in authors)
            {
                sb.AppendLine(author.FullName);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            /*
              Return the titles of the book, 
              which contain a given string. 
              Ignore casing.
              Return all titles in a single string,
              each on a new row, ordered alphabetically.
             */

            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b.Title)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            /*
             * Return all titles of books and their authors'
             * names for books, which are written by authors
             * whose last names start with the given string.
             * Return a single string with each title on a new row.
             * Ignore casing.
             * Order by BookId ascending.
             */

            var books = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    AuthorFullName = $"{b.Author.FirstName} {b.Author.LastName}"
                })
                .ToArray();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.AuthorFullName})");
            }

            return sb.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books
                .Where(b => b.Title.Length > lengthCheck);
            /*
             * Return the number of books, 
             * which have a title longer 
             * than the number given as an
             * input.
             */

            return books.Count();
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {

            StringBuilder sb = new StringBuilder();
            /*
             * Return the total number of book copies for each author.
             * Order the results descending by total book copies.
             * Return all results in a single string, each on a new line.
             */

            var authors = context.Authors.
                Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName,
                    BookCopies = a.Books.Select(b => b.Copies).Sum()                   
                })
                .OrderByDescending(b => b.BookCopies)
                .ToList();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FullName} - {author.BookCopies}");
            }

            return sb.ToString().TrimEnd();

        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categories = context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    Profit = c.CategoryBooks.Sum(cb => cb.Book.Price * cb.Book.Copies)
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name)
                .ToArray();

            /*
             * Return the total profit of all books by category.
             * Profit for a book can be calculated by multiplying 
             * its number of copies by the price per single book.
             * Order the results by descending by total profit for 
             * a category and ascending by category name. 
             * Print the total profit formatted to the second digit.
             */

            foreach (var category in categories)
            {
                sb.AppendLine($"{category.Name} ${category.Profit:f2}");
            }


            return sb.ToString().TrimEnd();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categories = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    c.Name,
                    Books = c.CategoryBooks.Select(cb => new
                    {
                        Title = cb.Book.Title,
                        ReleaseYear = cb.Book.ReleaseDate.Value.Year
                    })
                    .OrderByDescending(cb => cb.ReleaseYear)
                    .Take(3)
                    .ToList()
                })                
                .ToList();
            /*
             * Get the most recent books by categories.
             * The categories should be ordered by name alphabetically.
             * Only take the top 3 most recent books from each category
             * – ordered by release date(descending). Select and print
             * the category name and for each book – its title and release year.
             */

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Name}");

                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseYear})");
                }
            }


            return sb.ToString().TrimEnd();
        }
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010);

            /*
             * Increase the prices of all books released before 2010 by 5.
             */

            foreach (var book in books)
            {
                book.Price += 5;
            }
                
            context.SaveChanges();
        }
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            /*
             * Remove all books, which have less than 4200 copies. 
             * Return an int - 
             * the number of books that were deleted from the database.
             */

            int removedBooksCount = books.Count;

            context.RemoveRange(books);

            context.SaveChanges();

            return removedBooksCount;
            
        }
    }
}


