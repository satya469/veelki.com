using System;

namespace Veelki.Data.Entities
{
    public partial class Bets
    {
        public int Id { get; set; }
        public string BetId { get; set; }
        public int SportId { get; set; }
        public long EventId { get; set; }
        public string Event { get; set; }
        public string MarketId { get; set; }
        public string Market { get; set; }
        public long SelectionId { get; set; }
        public string Selection { get; set; }
        public int OddsType { get; set; }  // 1 for Match Odds. 2 for Bookmaker. 3 for Fancy Odds.
        public string Type { get; set; }  // Back or Lay
        public double OddsRequest { get; set; }
        public float AmountStake { get; set; }
        public int BetType { get; set; }  // Back 1,2,3 and Lay 1,2,3
        public DateTime PlaceTime { get; set; }
        public DateTime MatchedTime { get; set; }
        public DateTime SettleTime { get; set; }
        public int IsSettlement { get; set; } // 1 for Settle. 2 for Unsettle. 3 for Void
        public bool Status { get; set; }  // Show bet true or false.
        public int UserId { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? ResultType { get; set; }  // 1 for Win and 2 for Loss 3 for Draw 4 for Tie 5 for no result
        public float? ResultAmount { get; set; } // Profit and Loss Amout
    }
}
