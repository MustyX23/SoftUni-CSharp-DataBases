using AutoMapper;
using Invoices.Data.Models;
using Invoices.DataProcessor.ExportDto;
using Invoices.DataProcessor.ImportDto;
using System.Globalization;

namespace Invoices
{
    public class InvoicesProfile : Profile
    {
        public InvoicesProfile()
        {
            CreateMap<ImportClientDTO, Client>()
                .ForMember(d => d.Addresses, opt => opt.Ignore());
            CreateMap<ImportClientAddressDTO, Address>();

            CreateMap<ImportInvoiceDTO, Invoice>();

            CreateMap<ImportProductDTO, Product>();

            CreateMap<Client, ClientExportDto>()
                .ForMember(dest => dest.Invoices, opt =>
                    opt.MapFrom(s => s.Invoices
                                        .ToArray()
                                        .OrderBy(i => i.IssueDate)
                                        .ThenByDescending(i => i.DueDate)));

           //CreateMap<Invoice, ClientInvoiceExportDto>()
           //    .ForMember(dest => dest.DueDate, opt =>
           //        opt.MapFrom(s => s.DueDate.ToString("d", CultureInfo.InvariantCulture)))
           //    .ForMember(dest => dest.Currency, opt => 
           //    opt.MapFrom(s => s.CurrencyType.ToString()));

        }
    }
}
