namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();

            ImportCreatorsDto[] creatorsDtos
                = xmlHelper.Deserialize<ImportCreatorsDto[]>(xmlString, "Creators");

            var creators = new HashSet<Creator>();

            foreach (var creatorDto in creatorsDtos)
            {
                if (!IsValid(creatorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var creator = new Creator() 
                {
                    FirstName = creatorDto.FirstName,
                    LastName = creatorDto.LastName,
                };

                foreach (var boardGameDto in creatorDto.Boardgames)
                {
                    if (!IsValid(boardGameDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Boardgame boardgame = new Boardgame()
                    {
                        Name = boardGameDto.Name,
                        Rating = boardGameDto.Rating,
                        YearPublished = boardGameDto.YearPublished,
                        CategoryType = (Data.Models.Enums.CategoryType)boardGameDto.CategoryType,
                        Mechanics = boardGameDto.Mechanics
                    };

                    creator.Boardgames.Add(boardgame);
                }

                creators.Add(creator);
                sb.AppendLine(String.Format
                    (SuccessfullyImportedCreator, creator.FirstName, creator.LastName
                    , creator.Boardgames.Count));
            }

            context.Creators.AddRange(creators);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
            

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            ImportSellersDto[] sellersDtos =
                JsonConvert.DeserializeObject<ImportSellersDto[]>(jsonString);

            var validBoardGamesIds = context.Boardgames
                .Select(x => x.Id).ToArray();

            var sellers = new HashSet<Seller>();

            foreach (var sellerDto in sellersDtos)
            {
                if (!IsValid(sellerDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Seller seller = new Seller() 
                {
                    Name = sellerDto.Name,
                    Address = sellerDto.Address,
                    Country = sellerDto.Country,
                    Website = sellerDto.Website,
                };

                foreach (int boardgameId in sellerDto.Boardgames.Distinct())
                {
                    if (!validBoardGamesIds.Contains(boardgameId))
                    {
                        stringBuilder.AppendLine(ErrorMessage);
                        continue;
                    }

                    BoardgameSeller boardgameSeller = new BoardgameSeller()
                    {
                        Seller = seller,
                        BoardgameId = boardgameId,
                    };

                    seller.BoardgamesSellers.Add(boardgameSeller);
                }

                sellers.Add(seller);
                stringBuilder.AppendLine(String.Format
                    (SuccessfullyImportedSeller, seller.Name, 
                    seller.BoardgamesSellers.Count));
            }

            context.Sellers.AddRange(sellers);
            context.SaveChanges();
            return stringBuilder.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
