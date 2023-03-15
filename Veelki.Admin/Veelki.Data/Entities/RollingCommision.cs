namespace Veelki.Data.Entities
{
    public partial class RollingCommision
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public double Fancy { get; set; }
        public double Matka { get; set; }
        public double Casino { get; set; }
        public double Binary { get; set; }
        public double SportBook { get; set; }
        public double Bookmaker { get; set; }
    }
}
