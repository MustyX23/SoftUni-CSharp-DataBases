using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarDealer.DTOs.Export
{
    public class CarDTO
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public long TraveledDistance { get; set; }

        [JsonIgnore]
        public List<int> PartsId { get; set; }
    }
}
