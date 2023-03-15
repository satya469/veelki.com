using System;

namespace Veelki.Data.Entities
{
    public partial class Matches
    {
        public long eventId { get; set; }
        public int SportId { get; set; }
        public string eventName { get; set; }
        public long SeriesId { get; set; }
        public DateTime MatchDate { get; set; }
        public string marketId { get; set; }
        public bool Status { get; set; }
        public double MinStack { get; set; }
        public double MaxStack { get; set; }
        public int OddsLimit { get; set; }
    }
}
