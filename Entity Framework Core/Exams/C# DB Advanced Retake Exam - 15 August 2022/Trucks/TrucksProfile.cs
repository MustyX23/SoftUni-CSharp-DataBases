namespace Trucks
{
    using AutoMapper;
    using Trucks.Data.Models;
    using Trucks.DataProcessor.ExportDto;
    using Trucks.DataProcessor.ImportDto;

    public class TrucksProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE OR RENAME THIS CLASS
        public TrucksProfile()
        {
            CreateMap<ImportDespatcherDto, Despatcher>()
                .ForMember(dest => dest.Trucks, opt => opt.Ignore());

            CreateMap<ImportDespetcherTruckDto, Truck>();


            //CreateMap<Client, ExportClientDTO>()
            //    .ForMember(dest => dest.Trucks, opt => opt.MapFrom(d => d.ClientsTrucks
            //    .Select(ct => ct.Truck)
            //    .OrderBy(t => t.MakeType)
            //    .ThenByDescending(t => t.CargoCapacity)));
            //
            //CreateMap<Truck, ExportClientTruckDto>()
            //    .ForMember(dest => dest );

        }
    }
}
