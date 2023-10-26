using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Game
    {
        public int GameId { get; set; }

        public int HomeTeamId {  get; set; }

        public int AwayTeamId { get; set; }

        public byte HomeTeamGoals { get; set; }

        public byte AwayTeamGoals { get; set; }
        
        public DateTime DateTime { get; set; }

        public double HomeTeamBetRate {  get; set; }

        public double AwayTeamBetRate { get; set; }

        public double DrawBetRate { get; set; }

        public string? Result { get; set; }
    }
}
