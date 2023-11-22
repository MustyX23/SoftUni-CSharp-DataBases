using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artillery.Data.Models
{
    public class Country
    {
        public Country()
        {
            CountriesGuns = new HashSet<CountryGun>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(60)]
        public string CountryName { get; set; } = null!;

        [Range(50000, 10000000)]
        [Required]
        public int ArmySize { get; set; }

        public ICollection<CountryGun> CountriesGuns { get; set; } = null!;
    }
}
