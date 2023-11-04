using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Invoices.Data.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string StreetName { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public string PostCode { get; set; }

        [Required]
        [MaxLength(15)]
        public string City { get; set; }

        [Required]
        [MaxLength(15)]
        public string Country { get; set; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

    }
}
