using Eventmi.Infrastructure.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmi.Infrastructure.Data.Common
{
    public interface IRepository
    {
        Task AddAsync<T>(T entity) where T : class;        

        IQueryable<T> All<T>() where T : class;

        IQueryable<T> AllReadOnly<T>() where T : class;

        IQueryable<T> AllWithDeleted<T>() where T : class, IDeletable;

        IQueryable<T> AllWithDeletedReadOnly<T>() where T : class, IDeletable;

        void Delete<T>(T entity) where T : class, IDeletable;

        Task<int> SaveChangesAsync();
    }
}
