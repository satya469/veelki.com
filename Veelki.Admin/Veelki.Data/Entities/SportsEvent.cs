using System;

namespace Veelki.Data.Entities
{
    public partial class SportsEvent
    {
        public string gameId { get; set; }
        public string marketId { get; set; }
        public string eid { get; set; }
        public string eventName { get; set; }
        public long tournamentId { get; set; }
        public DateTime eventDate { get; set; }
        public string inPlay { get; set; }
        public double totalMatched { get; set; }
        public string tv { get; set; }
        public double back1 { get; set; }
        public double lay1 { get; set; }
        public double back11 { get; set; }
        public double lay11 { get; set; }
        public double back12 { get; set; }
        public double lay12 { get; set; }
        public string m1 { get; set; }
        public string f { get; set; }
        public int vir { get; set; }
        public bool IsPinnedMatch { get; set; }
        public bool DisableMatch { get; set; }
    }
}
