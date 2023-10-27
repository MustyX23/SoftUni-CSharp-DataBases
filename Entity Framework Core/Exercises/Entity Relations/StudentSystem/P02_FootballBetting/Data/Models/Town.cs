using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Town 
    {

        public Town()
        {
            Teams = new HashSet<Team>();
        }
        public int TownId { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
