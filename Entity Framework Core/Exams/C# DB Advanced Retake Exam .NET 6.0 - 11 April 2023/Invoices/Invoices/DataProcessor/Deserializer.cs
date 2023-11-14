namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using AutoMapper;
    using Castle.Core.Internal;
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.DataProcessor.ImportDto;
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
            XmlHelper xmlHelper = new XmlHelper();

            IMapper mapper = InitializeMapper();

            var clients = new HashSet<Client>();
            var clientsDTOS = xmlHelper.Deserialize<ImportClientDTO[]>(xmlString, "Clients");

            var errorMessages = new List<string>();

            foreach (var clientDTO in clientsDTOS)
            {
                if (string.IsNullOrEmpty(clientDTO.Name) || string.IsNullOrEmpty(clientDTO.NumberVat))
                {
                    errorMessages.Add("Invalid data!");
                    continue;
                }

                Client client = mapper.Map<Client>(clientDTO);


                foreach (var addressDTO in clientDTO.Addresses)
                {
                    if (string.IsNullOrEmpty(addressDTO.StreetName) ||
                        addressDTO.StreetNumber <= 0 ||
                        string.IsNullOrEmpty(addressDTO.PostCode) ||
                        string.IsNullOrEmpty(addressDTO.City) ||
                        string.IsNullOrEmpty(addressDTO.Country))
                    {
                        errorMessages.Add("Invalid data!");
                        continue;
                    }

                    Address address = mapper.Map<Address>(addressDTO);

                    client.Addresses.Add(address);
                    
                }

                clients.Add(client);
            }
            return $"{string.Join(Environment.NewLine, errorMessages)}{Environment.NewLine}Successfully imported {clients.Count} clients.";
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {


            throw new NotImplementedException();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
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
