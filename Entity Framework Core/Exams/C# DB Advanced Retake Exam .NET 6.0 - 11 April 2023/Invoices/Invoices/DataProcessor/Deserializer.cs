namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using AutoMapper;
    using Castle.Core.Internal;
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using static System.Net.Mime.MediaTypeNames;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";


        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();

            IMapper mapper = InitializeMapper();

            var clients = new HashSet<Client>();
            var clientsDTOS = xmlHelper.Deserialize<ImportClientDTO[]>(xmlString, "Clients");

            foreach (var clientDTO in clientsDTOS)
            {
                if (!IsValid(clientDTO))
                {
                    sb.AppendLine("Invalid data!");
                    continue;
                }

                Client client = mapper.Map<Client>(clientDTO);


                foreach (var addressDTO in clientDTO.Addresses)
                {
                    if (!IsValid(addressDTO))
                    {
                        sb.AppendLine("Invalid data!");
                        continue;
                    }

                    Address address = mapper.Map<Address>(addressDTO);

                    client.Addresses.Add(address);
                    
                }

                clients.Add(client);
                sb.AppendLine(String.Format(SuccessfullyImportedClients, client.Name));
            }
            context.Clients.AddRange(clients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            //Invoives are 100 but must be 55!!!
            IMapper mapper = InitializeMapper();

            StringBuilder sb = new StringBuilder();

            var invoicesDTOS = JsonConvert.DeserializeObject<ImportInvoiceDTO[]>(jsonString);

            var invoices = new HashSet<Invoice>();

            foreach (var invoiceDTO in invoicesDTOS)
            {
                if (!IsValid(invoiceDTO))
                {
                    sb.AppendLine("Invalid data!");
                    continue;
                }

                if (invoiceDTO.DueDate == DateTime.ParseExact
                    ("01/01/0001", "dd/MM/yyyy", CultureInfo.InvariantCulture) ||
                    invoiceDTO.IssueDate == DateTime.ParseExact("01/01/0001", "dd/MM/yyyy", CultureInfo.InvariantCulture))
                {
                    sb.AppendLine("Invalid data!");
                    continue;
                }

                Invoice invoice = mapper.Map<Invoice>(invoiceDTO);
                invoices.Add(invoice);
                sb.AppendLine(String.Format(SuccessfullyImportedInvoices, invoice.Number));
            }

            context.Invoices.AddRange(invoices);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {


            throw new NotImplementedException();
        }

        private static IMapper InitializeMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<InvoicesProfile>();
            }));                      
        }
        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    } 
}
