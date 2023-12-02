using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Medicines.Data.Models
{
    public class Medicine
    {
        public Medicine()
        {
            PatientsMedicines = new HashSet<PatientMedicine>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0.01, 1000.00)]
        public decimal Price { get; set; }

        [Required]
        public Category Category {  get; set; }

        [Required]
        public DateTime ProductionDate {  get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Producer { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Pharmacy))]
        public int PharmacyId {  get; set; }

        public Pharmacy Pharmacy { get; set; } = null!;

        public ICollection<PatientMedicine> PatientsMedicines { get; set; } = null!;

    }
}