﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        public int GameId {  get; set; }

        public int PlayerId { get; set; }

        public byte ScoredGoals {  get; set; }

        public byte Assists {  get; set; }

        public byte MinutesPlayed { get; set; }

    }
}
