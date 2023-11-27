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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventmiDbContext).Assembly);
        }
    }
}
