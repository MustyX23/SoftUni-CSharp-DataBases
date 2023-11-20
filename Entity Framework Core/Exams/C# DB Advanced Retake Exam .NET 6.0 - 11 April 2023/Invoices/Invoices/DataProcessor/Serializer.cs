namespace Invoices.DataProcessor
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
                .Where(c => c.Invoices.Any(i => i.IssueDate > date))
                .ToArray()
                .Select(c => new ClientExportDto()
                {
                    Name = c.Name,
                    NumberVat = c.NumberVat,
                    InvoicesCount = c.Invoices.Count,
                    Invoices = c.Invoices
                    .OrderBy(i => i.IssueDate)
                    .ThenByDescending(i => i.DueDate)
                    .ToArray()
                    .Select(i => new ClientInvoiceExportDto()
                    {
                        Number = i.Number,
                        Amount = i.Amount,
                        DueDate = i.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        Currency = i.CurrencyType.ToString(),
                        IssueDate = i.IssueDate                       
                     })                  
                    .ToArray()
                })
                .OrderByDescending(c => c.InvoicesCount)
                .ThenBy(c => c.Name)
                .ToArray();

            return xmlHelper.Serialize<ClientExportDto[]>(clients, "Clients");
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {
            var products = context.Products
                .Where(p => p.ProductsClients.Any(pc => pc.Client.Name.Length >= nameLength))
                .Select(p => new
                {
                    Name = p.Name,
                    Price = Decimal.Parse($"{p.Price:f2}"),
                    Category = p.CategoryType.ToString(),
                    Clients = p.ProductsClients
                    .Where(p => p.Client.Name.Length >= nameLength)
                    .Select(pc => new
                    {
                        Name = pc.Client.Name,
                        NumberVat = pc.Client.NumberVat
                    })
                    .OrderBy(p => p.Name)
                    .ToArray()
                })
                .OrderByDescending(p => p.Clients.Count())
                .ThenBy(p => p.Name)
                .Take(5)
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