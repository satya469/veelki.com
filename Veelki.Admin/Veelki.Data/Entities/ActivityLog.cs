using System;

namespace Veelki.Data.Entities
{
    public partial class ActivityLog
    {
        public int Id { get; set; }
        public DateTime LoginDate { get; set; }
        //public int Login_status { get; set; } // 1. for online 2. for offline
        //public DateTime LogOutDate { get; set; }
        public string IpAddress { get; set; }
        public string ISP { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
    }
}
