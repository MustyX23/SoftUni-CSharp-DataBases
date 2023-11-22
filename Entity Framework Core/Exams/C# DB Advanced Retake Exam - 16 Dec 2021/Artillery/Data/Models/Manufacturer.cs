using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artillery.Data.Models
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            Guns = new HashSet<Gun>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(40)]
        public string ManufacturerName { get; set; } = null!;

        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string Founded { get; set; } = null!;

        public ICollection<Gun> Guns { get; set; } = null!;
    }
}
