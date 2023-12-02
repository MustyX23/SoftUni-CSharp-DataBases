using Eventmi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmi.Core.Contracts
{
    public interface IEventService
    {
        Task CreateAsync(EventModel model);

        Task DeleteAsync(int id);

        Task EditAsync(EventModel model);

        Task<EventModel> GetByIdAsync(int id);

        Task<IEnumerable<EventModel>> GetAllAsync();
    }
}
