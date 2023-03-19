using System.ComponentModel.DataAnnotations;

namespace Veelki.Data.Entities
{
    public partial class Series
    {
        [Key]
        public long tournamentId { get; set; }
        public int SportId { get; set; }
        public string tournamentName { get; set; }
        public bool Status { get; set; }
    }
}
