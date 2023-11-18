using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Trucks.DataProcessor.ExportDto
{
    public class ExportClientTruckDto
    {
        [JsonProperty("TruckRegistrationNumber")]
        public string? RegistrationNumber { get; set; }

        public string? VinNumber {  get; set; }

        public int TankCapacity {  get; set; }

        public int CargoCapacity { get; set; }

        public string? CategoryType {  get; set; }

        public string? MakeType { get; set; }
    }
}
