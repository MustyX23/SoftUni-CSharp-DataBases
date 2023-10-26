using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? LogoUrl {  get; set; }

        public string Initials { get; set; }

        public decimal Budget { get; set; }

        public int PrimaryKitColorId {  get; set; }

        public int SecondaryKitColorId { get;set; }

        public int TownId { get; set; }


    }
}
