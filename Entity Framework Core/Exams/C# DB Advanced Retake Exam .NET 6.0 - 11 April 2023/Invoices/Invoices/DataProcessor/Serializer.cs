﻿namespace Invoices.DataProcessor
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Linq;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            XmlHelper xmlHelper = new XmlHelper();

            //IMapper mapper = InitializeMapper();
            //
            //ClientExportDto[] clients = context.Clients
            //    .Where(c => c.Invoices.Any(i => i.IssueDate >= date))
            //    .ProjectTo<ClientExportDto>(mapper.ConfigurationProvider)
            //    .OrderByDescending(c => c.InvoicesCount)
            //    .ThenBy(c => c.Name)
            //    .ToArray();

            ClientExportDto[] clients = context.Clients
                .Where(c => c.Invoices.Any(i => i.IssueDate >= date))
                .Select(c => new ClientExportDto()
                {
                    Name = c.Name,
                    NumberVat = c.NumberVat,
                    InvoicesCount = c.Invoices.Count,
                    Invoices = c.Invoices.Select(i => new ClientInvoiceExportDto()
                    {
                        Number = i.Number,
                        Amount = i.Amount,
                        DueDate = DateTime.ParseExact(i.DueDate, "d", CultureInfo.InvariantCulture),
                        Currency = Enum.Parsei.CurrencyType
                     })
                })
                .ToArray();

            return xmlHelper.Serialize<ClientExportDto[]>(clients, "Clients");
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {
            var products = context.Products
                .Where(p => p.ProductsClients.Any(pc => pc.Client.Name.Length == nameLength))
                .Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.CategoryType.ToString(),
                    Clients = p.ProductsClients.Select(pc => new
                    {
                        Name = pc.Client.Name,
                        NumberVat = pc.Client.NumberVat
                    })
                    .OrderBy(p => p.Name)
                    .ToArray()
                })
                .OrderByDescending(p => p.Clients.Count())
                .ThenBy(p => p.Name)
                .ToArray();

            return JsonConvert.SerializeObject(products, Formatting.Indented);
        }
        private static IMapper InitializeMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<InvoicesProfile>();
            }));
        }
    }
}