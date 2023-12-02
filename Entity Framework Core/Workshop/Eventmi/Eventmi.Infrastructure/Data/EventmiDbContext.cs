using Microsoft.EntityFrameworkCore;

namespace Eventmi.Infrastructure.Data
{
    public class EventmiDbContext : DbContext
    {
        public EventmiDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public EventmiDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=Eventmi;Trusted_Connection=true;TrustServerCertificate=true");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventmiDbContext).Assembly);
        }
    }
}
