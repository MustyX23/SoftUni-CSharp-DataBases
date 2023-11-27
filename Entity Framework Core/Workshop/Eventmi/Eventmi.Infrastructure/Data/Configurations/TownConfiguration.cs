using Eventmi.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventmi.Infrastructure.Data.Configurations
{
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        private readonly string[] towns = new string[] 
        {
            "София", "Пловдив", "Варна", "Бургас", "Велинград"
        };

        public void Configure(EntityTypeBuilder<Town> builder)
        {
            List<Town> entities = new List<Town>();

            int townId = 1;

            foreach (var town in towns)
            {
                entities.Add(new Town
                {
                    Id = townId,
                    Name = town
                });

                townId++;
            }

            builder.HasData(entities);
        }
    }
}
