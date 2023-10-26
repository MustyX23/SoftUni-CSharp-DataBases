using P02_FootballBetting.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Bet
    {
        public int BetId { get; set; }

        public decimal Amount {  get; set; }

        public Prediction Prediction { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        public int GameId { get; set; }
    }
}
