using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucks.Data.Models
{
    public class ClientTruck
    {
        [Required]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = null!;

        [Required]
        public int TruckId { get; set; }

        public virtual Truck Truck { get; set; } = null!;

    }
}
