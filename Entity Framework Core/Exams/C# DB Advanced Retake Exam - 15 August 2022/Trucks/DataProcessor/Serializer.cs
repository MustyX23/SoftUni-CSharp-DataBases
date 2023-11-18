namespace Trucks.DataProcessor
{
    using Data;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Trucks.Data.Models;
    using Trucks.DataProcessor.ExportDto;
    using AutoMapper.QueryableExtensions;
    using AutoMapper;
    using Newtonsoft.Json;
    using Trucks.Data.Models.Enums;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();

            ExportDespatcherDto[] despatchers = context.Despatchers
                .Where(d => d.Trucks.Any())
                .ToArray()
                .Select(d => new ExportDespatcherDto()
                {
                    Name = d.Name,
                    TrucksCount = d.Trucks.Count(),
                    Trucks = d.Trucks.Select(t => new ExportTruckDespatcherDto()
                    {
                        RegistrationNumber = t.RegistrationNumber,
                        MakeType = t.MakeType.ToString()
                    })
                    .OrderBy(t => t.RegistrationNumber)
                    .ToArray()
                    
                })
                .OrderByDescending(d => d.TrucksCount)
                .ThenBy(d => d.Name)
                .ToArray();

            return xmlHelper.Serialize<ExportDespatcherDto[]>(despatchers, "Despatchers");
        }
        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
           //IMapper mapper = InitializeMapper();
           //ExportClientDTO[] clients = context.Clients
           //    .Where(c => c.ClientsTrucks.Any(ct => ct.Truck.TankCapacity >= capacity))
           //    .ProjectTo<ExportClientDTO>(mapper.ConfigurationProvider)
           //    .Take(10)
           //    .ToArray();

            var clients = context.Clients
                .Where(c => c.ClientsTrucks.Any(ct => ct.Truck.TankCapacity >= capacity))
                .ToArray()
                .Select(c => new
                {
                    Name = c.Name,
                    Trucks = c.ClientsTrucks
                    .Where(ct => ct.Truck.TankCapacity >= capacity)
                    .Select(ct => new
                    {
                        TruckRegistrationNumber = ct.Truck.RegistrationNumber,
                        VinNumber = ct.Truck.VinNumber,
                        TankCapacity = ct.Truck.TankCapacity,
                        CargoCapacity = ct.Truck.CargoCapacity,
                        CategoryType = ct.Truck.CategoryType.ToString(),
                        MakeType = ct.Truck.MakeType.ToString()
                    })
                    .OrderBy(t => t.MakeType)
                    .ThenByDescending(t => t.CargoCapacity)
                    .ToArray()
                })
                .OrderByDescending(c => c.Trucks.Count())
                .ThenBy(c => c.Name)
                .Take(10)
                .ToArray();

            return JsonConvert.SerializeObject(clients, Formatting.Indented);
            //Select the top 10 clients that have at least one 
            //truck that their tank capacity is bigger or equal
            //to the given capacity.Select them with their
            //trucks which meet the same criteria
            //(their tank capacity is bigger or equal to the given one).
            //For each client, export their name and their trucks. For each truck, export its registration number, VIN number, tank capacity, cargo capacity, category and make type. Order the trucks by make type(ascending), then by cargo capacity(descending). Order the clients by all trucks(meeting above condition) count(descending), then by name(ascending).
            //NOTE: You may need to call.ToArray() function before the selection 
            //in order to detach entities from the database and avoid runtime
            //errors(EF Core bug).

        }
        private static IMapper InitializeMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TrucksProfile>();
            }));
        }
    }
}
