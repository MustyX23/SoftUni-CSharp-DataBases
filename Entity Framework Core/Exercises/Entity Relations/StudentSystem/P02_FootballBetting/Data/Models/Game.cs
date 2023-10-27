using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Game
    {
        public Game()
        {
            PlayersStatistics = new HashSet<PlayerStatistic>();
            Bets = new HashSet<Bet>();
        }
        public int GameId { get; set; }

        public int HomeTeamId { get; set; }

        public virtual Team HomeTeam { get; set; }
        public int AwayTeamId { get; set; }
        public virtual Team AwayTeam { get; set; }

        public byte HomeTeamGoals { get; set; }

        public byte AwayTeamGoals { get; set; }
        
        public DateTime DateTime { get; set; }

        public double HomeTeamBetRate {  get; set; }

        public double AwayTeamBetRate { get; set; }

        public double DrawBetRate { get; set; }

        public string? Result { get; set; }

        public ICollection<PlayerStatistic> PlayersStatistics { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
