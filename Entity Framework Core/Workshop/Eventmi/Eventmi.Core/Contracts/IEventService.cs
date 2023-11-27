using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmi.Core.Contracts
{
    public interface IEventService
    {
        Task CreateAsync();
    }
}
