using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Team
    {
        public Team()
        {
            HomeGames = new HashSet<Game>();
            AwayGames = new HashSet<Game>();
        }

        public int TeamId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? LogoUrl {  get; set; }

        public string Initials { get; set; }

        public decimal Budget { get; set; }

        public int PrimaryKitColorId {  get; set; }

        public virtual Color PrimaryKitColor{ get; set; }

        public int SecondaryKitColorId { get;set; }

        public virtual Color SecondaryKitColor { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        public ICollection<Game> HomeGames { get; set; }

        public ICollection<Game> AwayGames { get; set; }

        public ICollection<Player> Players { get; set; }


    }
}
