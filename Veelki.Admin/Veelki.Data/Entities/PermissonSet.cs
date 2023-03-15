using System;
using System.ComponentModel.DataAnnotations;

namespace Veelki.Data.Entities
{
    public partial class PermissonSet
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public bool? isdeleted { get; set; }
        public bool isactive { get; set; }
    }
}
