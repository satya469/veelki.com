namespace RB444.Model.Model
{
    public class ProfitAndLossVM
    {
        public int Id { get; set; }
        public string BetId { get; set; }
        public string SportName { get; set; }
        public string Event { get; set; }
        public string Market { get; set; }
        public string Selection { get; set; }
        public string OddsType { get; set; }
        public string Type { get; set; } 
        public float OddsRequest { get; set; }
        public float AmountStake { get; set; }
        public string ResultType { get; set; } 
        public float ResultAmount { get; set; }
    }
}
