using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniORM_Core
{
    public abstract class DbContext
    {
        private readonly DatabaseConnection connection;
    }
}
