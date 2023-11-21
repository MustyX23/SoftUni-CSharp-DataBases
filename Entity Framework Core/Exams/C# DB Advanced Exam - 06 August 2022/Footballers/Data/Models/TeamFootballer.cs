using System.ComponentModel.DataAnnotations;

namespace Footballers.Data.Models
{
    public class TeamFootballer
    {
        [Required]
        public int TeamId {  get; set; }

        public Team Team { get; set; } = null!;

        [Required]
        public int FootballerId {  get; set; }

        public Footballer Footballer { get; set; } = null!;
    }
}