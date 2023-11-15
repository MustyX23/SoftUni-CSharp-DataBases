using AutoMapper;
using Invoices.Data.Models;
using Invoices.DataProcessor.ImportDto;

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

        }
    }
}
