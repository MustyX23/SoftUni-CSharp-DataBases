using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Medicines.Data.Models
{
    public class Pharmacy
    {
        public Pharmacy()
        {
            Medicines = new HashSet<Medicine>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(14)]
        [MaxLength(14)]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public bool IsNonStop {  get; set; }

        public ICollection<Medicine> Medicines { get; set; } = null!;


    }
}
