namespace RB444.Model.Model
{
    public class CreditReferenceVM
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public double OldBalance { get; set; }
        public double NewBalance { get; set; }
    }
}
