using System;
using System.Collections.Generic;
using System.Text;

namespace RB444.Model.Model
{
   public class MarketWatchVM
    {
        public string FullName { get; set; }
        public string Event { get; set; }
        public int OddsType { get; set; } // 1 for Match Odds. 2 for Bookmaker. 3 for Fancy Odds.
        public float OddsRequest { get; set; }
        public float AmountStake { get; set; }
        public int? ResultType { get; set; }  // 1 for Win and 2 for Loss 3 from Draw/Tie
        public float? MatchFinishedAmount { get; set; }
        public string Runner { get; set; }
    }
}
