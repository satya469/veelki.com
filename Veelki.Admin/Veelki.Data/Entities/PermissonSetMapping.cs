using System;

namespace Veelki.Data.Entities
{
    public partial class PermissonSetMapping
    {
        public int id { get; set; }
        public int formid { get; set; }
        public int permissionsetid { get; set; }
        public bool isread { get; set; }
        public bool iswrite { get; set; }
        public bool isdenied { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public bool? isdeleted { get; set; }
        public bool isactive { get; set; }
        public virtual Forms Forms { get; set; }
        public virtual PermissonSet PermissonSet { get; set; }
    }
}
