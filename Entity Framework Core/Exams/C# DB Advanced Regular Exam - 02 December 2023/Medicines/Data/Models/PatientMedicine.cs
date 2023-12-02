using System.ComponentModel.DataAnnotations;

namespace Medicines.Data.Models
{
    public class PatientMedicine
    {
        [Required]
        public int PatientId {  get; set; }

        public Patient Patient { get; set; } = null!;

        [Required]
        public int MedicineId { get; set; }

        public Medicine Medicine { get; set; } = null!;
    }
}