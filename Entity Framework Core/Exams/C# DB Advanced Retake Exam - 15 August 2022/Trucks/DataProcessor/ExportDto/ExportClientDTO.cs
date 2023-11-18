using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucks.DataProcessor.ImportDto;

namespace Trucks.DataProcessor.ExportDto
{
    public class ExportClientDTO
    {
        public string? Name {  get; set; }

        public ExportClientTruckDto[] Trucks { get; set; } = null!;
    }
}
