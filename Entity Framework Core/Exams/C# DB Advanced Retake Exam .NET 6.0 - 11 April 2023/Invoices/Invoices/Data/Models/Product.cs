using Invoices.Data.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class Product
    {
        public Product()
        {
            ProductsClients = new HashSet<ProductClient>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }

        public virtual ICollection<ProductClient> ProductsClients { get; set; } = null!;

    }
}
