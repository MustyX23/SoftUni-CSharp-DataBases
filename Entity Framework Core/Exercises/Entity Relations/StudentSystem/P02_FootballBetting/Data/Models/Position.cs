using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Position
    {
        public Position()
        {
            Players = new HashSet<Player>();    
        }
        public int PositionId {  get; set; }
        public string Name { get; set; }

        public ICollection<Player> Players { get; set;}
    }
}
