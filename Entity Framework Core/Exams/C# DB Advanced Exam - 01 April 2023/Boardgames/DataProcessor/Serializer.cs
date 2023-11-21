namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using System.Xml.Linq;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();

            ExportCreatorsDto[] creators = context.Creators
                .Where(c => c.Boardgames.Any())               
                .ToArray()
                .Select(c => new ExportCreatorsDto()
                {
                    CreatorName = c.FirstName + " " + c.LastName,
                    BoardgamesCount = c.Boardgames.Count(),
                    Boardgames = c.Boardgames
                    .OrderBy(b => b.Name)
                    .ToArray()
                    .Select(b => new ExportBoardGameDto
                    {
                        BoardgameName = b.Name,
                        BoardgameYearPublished = b.YearPublished
                    })
                    .ToArray()

                })
                .OrderByDescending(b => b.BoardgamesCount)
                .ThenBy(b => b.CreatorName)
                .ToArray();

            return xmlHelper.Serialize<ExportCreatorsDto[]>(creators, "Creators");
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var sellers = context.Sellers
                .Where(s => s.BoardgamesSellers.Any
                (bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating))
                .ToArray()
                .Select(s => new
                {
                    Name = s.Name,
                    Website = s.Website,
                    Boardgames = s.BoardgamesSellers
                    .OrderByDescending(s => s.Boardgame.Rating)
                    .ThenBy(s => s.Boardgame.Name)
                    .Where(s => s.Boardgame.YearPublished >= year && s.Boardgame.Rating <= rating)
                    .Select(bs => new
                    {
                        Name = bs.Boardgame.Name,
                        Rating = bs.Boardgame.Rating,
                        Mechanics = bs.Boardgame.Mechanics,
                        Category = bs.Boardgame.CategoryType.ToString()
                    })
                    .ToArray()
                })
                .OrderByDescending(s => s.Boardgames.Length)
                .ThenBy(s => s.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(sellers, Formatting.Indented);
        }
    }
}