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
            CreateMap<Car, ExportBMWCarDTO>()
                .ForMember(s => s.CarId, opt => opt.MapFrom(s => s.Id));

            CreateMap<Supplier, ExportLocalSuppliersDTO>();

            CreateMap<Car, ExportCarWithListOfPartsDTO>()
                .ForMember(d => d.Parts, opt => opt.MapFrom(s => s.PartsCars
                    .Select(pc => pc.Part)
                    .OrderByDescending(p => p.Price)
                    .ToArray()));

            CreateMap<Part, ExportListOfPartsDTO>();

            this.CreateMap<Customer, ExportCustmerDTO>()
                .ForMember(dest => dest.BoughtCars, mo => mo.MapFrom(s => s.Sales.Count))
                .ForMember(dest => dest.SpentMoney,
                mo => mo.MapFrom(s => s.Sales.SelectMany(x => x.Car.PartsCars).Sum(x => x.Part.Price)));

        }
    }
}
