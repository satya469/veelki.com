using System;

namespace Veelki.Data.Entities
{
    public partial class UserStatus
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
    }
}
