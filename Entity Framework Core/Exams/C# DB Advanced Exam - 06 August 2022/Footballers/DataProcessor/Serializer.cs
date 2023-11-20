namespace Footballers.DataProcessor
{
    using Data;
    using Footballers.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Linq;
    using Footballers.DataProcessor.ExportDto;
    using Footballers.Data.Models.Enums;
    using System.Globalization;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();

            ExportCoachesDtos[] coaches = context.Coaches
                .Where(c => c.Footballers.Any())
                .ToArray()
                .Select(c => new ExportCoachesDtos()
                {
                    CoachName = c.Name,
                    FootballersCount = c.Footballers.Count,
                    Footballers = c.Footballers.Select(f => new ExportFootballerDto()
                    {
                        Name = f.Name,
                        Position = f.PositionType.ToString()
                    })
                    .OrderBy(c => c.Name)
                    .ToArray()
                })
                .OrderByDescending(c => c.FootballersCount)
                .ThenBy(c => c.CoachName)
                .ToArray();            

            return xmlHelper.Serialize<ExportCoachesDtos[]>(coaches, "Coaches");
            
        }
        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teams = context.Teams
                .Where(t => t.TeamsFootballers.Any(t => t.Footballer.ContractStartDate >= date))
                .ToArray()
                .Select(t => new
                {
                    t.Name,
                    Footballers = t.TeamsFootballers
                    .Where(tf => tf.Footballer.ContractStartDate >= date)
                    .OrderByDescending(tf => tf.Footballer.ContractEndDate)
                    .ThenBy(tf => tf.Footballer.Name)
                    .Select(tf => new
                    {
                        FootballerName= tf.Footballer.Name,
                        ContractStartDate = tf.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                        ContractEndDate = tf.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                        BestSkillType = tf.Footballer.BestSkillType.ToString(),
                        PositionType = tf.Footballer.PositionType.ToString()

                    })                    
                    .ToArray()
                })
                .OrderByDescending(t => t.Footballers.Length)
                .ThenBy(t => t.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(teams, Formatting.Indented);
            /*
             * Select the top 5 teams that have at least one footballer
             * that their contract start date is higher or equal to the given date.
             * Select them with their footballers who meet the same criteria
             * (their contract start date is after or equals the given date). 
             * For each team, export their name and their footballers. 
             * For each footballer, export their name and contract start date 
             * (must be in format "d"), contract end date (must be in format "d"),
             * position and best skill type. Order the footballers by contract end 
             * date (descending), then by name (ascending). Order the teams by all footballers (meeting above condition) count (descending), then by name (ascending).
               NOTE: Do not forget to use CultureInfo.InvariantCulture. You may need to call .ToArray() function before the selection in order to detach entities from the database and avoid runtime errors (EF Core bug). 

             */
        }
    }
}
