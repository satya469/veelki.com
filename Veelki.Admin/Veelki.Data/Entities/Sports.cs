namespace Veelki.Data.Entities
{
    public class Sports
    {
        public int Id { get; set; }
        public string SportName { get; set; }
        public long MinOddLimit { get; set; }
        public long MaxOddLimit { get; set; }
        public int MaxStackLimit { get; set; }
        public int BetDelayTime { get; set; }
        public bool Status { get; set; }
        public int Sequence { get; set; }
        public bool Highlight { get; set; }
    }
}
