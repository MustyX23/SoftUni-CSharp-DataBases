using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmi.Infrastructure.Data.Models
{
    [Comment("Място на провеждане")]
    public class Address
    {
        [Key]
        [Comment("Идентификатор на адреса")]
        public int Id { get; set; }

        [Required]
        [MaxLength(85)]
        [Comment("Улица")]
        public string Street { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Town))]
        [Comment("Идентификатор на град")]
        public int TownId { get; set; }
        public Town Town { get; set; } = null!; 
    }
}
