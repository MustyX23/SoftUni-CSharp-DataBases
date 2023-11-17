namespace Trucks
{
    using AutoMapper;
    using Trucks.Data.Models;
    using Trucks.DataProcessor.ImportDto;

    public class TrucksProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE OR RENAME THIS CLASS
        public TrucksProfile()
        {
            CreateMap<ImportDespatcherDto, Despatcher>()
                .ForMember(dest => dest.Trucks, opt => opt.Ignore());

            CreateMap<ImportDespetcherTruckDto, Truck>();                
        }
    }
}
