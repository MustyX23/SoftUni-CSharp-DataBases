using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Invoices.Data.Models
{
    public class Client
    {

        public Client()
        {
            Invoices = new HashSet<Invoice>();
            Addresses = new HashSet<Address>();
            ProductsClients = new HashSet<ProductClient>();
        }

        public int Id { get; set; }

        [MaxLength(25)]
        [MinLength(10)]
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        public string NumberVat { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        
        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<ProductClient> ProductsClients { get; set; }

    }
}