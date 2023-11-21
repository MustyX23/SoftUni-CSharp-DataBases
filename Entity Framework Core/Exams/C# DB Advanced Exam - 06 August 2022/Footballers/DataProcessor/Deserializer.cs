namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using Footballers.Data.Models;
    using Footballers.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();

            StringBuilder sb = new StringBuilder();

            var validCoaches = new HashSet<Coach>();
            var coachesDtos = xmlHelper.Deserialize<ImportCoachesDto[]>(xmlString, "Coaches");

            foreach (var coachDto in coachesDtos)
            {
                if (!IsValid(coachDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var coach = new Coach()
                {
                    Name = coachDto.Name,
                    Nationality = coachDto.Nationality                    
                };

                foreach (var f in coachDto.Footballers)
                {
                    if (!IsValid(f))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (DateTime.ParseExact(f.ContractStartDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture) > DateTime.ParseExact
                        (f.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Footballer footballer = new Footballer()
                    {
                        Name = f.Name,
                        ContractStartDate = DateTime.ParseExact
                    (f.ContractStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        ContractEndDate = DateTime.ParseExact
                    (f.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        BestSkillType = (Data.Models.Enums.BestSkillType)f.BestSkillType,
                        PositionType = (Data.Models.Enums.PositionType)f.PositionType
                    };

                    coach.Footballers.Add(footballer);
                }

                validCoaches.Add(coach);
                sb.AppendLine(String.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count));                             
            }

            context.Coaches.AddRange(validCoaches);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            ImportTeamsDto[] teamsDtos =
                JsonConvert.DeserializeObject<ImportTeamsDto[]>(jsonString);

            var teams = new HashSet<Team>();

            var validFootballerIds = context.Footballers
                .Select(f => f.Id).ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var teamDto in teamsDtos)
            {
                if (!IsValid(teamDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (teamDto.Trophies <= 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Team team = new Team() 
                {
                    Name = teamDto.Name,
                    Nationality = teamDto.Nationality,
                    Trophies = teamDto.Trophies,
                };

                foreach (var footballerId in teamDto.Footballers.Distinct())
                {
                    if (!validFootballerIds.Contains(footballerId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }


                    TeamFootballer teamFootballer = new TeamFootballer()
                    {
                        FootballerId = footballerId,
                        Team = team
                    };

                    team.TeamsFootballers.Add(teamFootballer);
                }

                teams.Add(team);
                sb.AppendLine(String.Format(SuccessfullyImportedTeam, team.Name, team.TeamsFootballers.Count));
            }

            context.Teams.AddRange(teams);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
