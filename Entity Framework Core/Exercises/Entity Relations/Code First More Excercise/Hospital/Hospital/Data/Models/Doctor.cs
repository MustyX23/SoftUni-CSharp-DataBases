using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Data.Models
{
    public class Doctor
    {
        public Doctor()
        {
            Visitations = new HashSet<Visitation>();
        }
        public int DoctorId { get; set; }

        [MaxLength(100)]
        public string Name { get; set;}

        public string Specialty { get; set;}

        public ICollection<Visitation> Visitations { get; set; }


    }
}