using System;

namespace Veelki.Data.Entities
{
    public partial class WorkspacePermissonMapping
    {
        public int id { get; set; }
        public int formid { get; set; }
        public int workspaceid { get; set; }
        public bool isread { get; set; }
        public bool iswrite { get; set; }
        public bool isdelete { get; set; }
        public bool isactive { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public bool? isdeleted { get; set; }
        public virtual Workspace Workspace { get; set; }
        public virtual Forms Forms { get; set; }
    }
}
