using AutoMapper;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<ImportSupplierDTO, Supplier>();

            CreateMap<ImportPartsDTO, Part>()
                .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.SupplierId.Value));

            CreateMap<ImportCarDTO, Car>()
                .ForSourceMember(d => d.Parts, opt => opt.DoNotValidate());

            CreateMap<ImportCustomerDTO, Customer>();

            CreateMap<ImportSaleDTO, Sale>()
                .ForMember(d => d.CarId,
                    opt => opt.MapFrom(s => s.CarId.Value));


            CreateMap<Car, ExportCarDTO>();

        }
    }
}
