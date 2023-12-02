using Microsoft.EntityFrameworkCore;

namespace Eventmi.Infrastructure.Data.Common
{
    public class Repository : IRepository
    {
        protected readonly EventmiDbContext context;

        public Repository(EventmiDbContext context)
        {
            this.context = context;
        }

        private DbSet<T> DbSet<T>() where T : class => context.Set<T>();

        public async Task AddAsync<T>(T entity) where T : class
        {
            await DbSet<T>().AddAsync(entity);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>();         
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return DbSet<T>().AsNoTracking();
        }

        IQueryable<T> IRepository.AllWithDeleted<T>()
        {
            return DbSet<T>()
                .IgnoreQueryFilters();
        }      

        IQueryable<T> IRepository.AllWithDeletedReadOnly<T>()
        {
            return DbSet<T>()
                .IgnoreQueryFilters()
                .AsNoTracking();
        }

        void IRepository.Delete<T>(T entity)
        {
            entity.IsActive = false;
            entity.DeletedOn = DateTime.Now;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<T?> GetById<T>(int id) where T : class
        {
            return await DbSet<T>().FindAsync(id);
        }
    }
}
