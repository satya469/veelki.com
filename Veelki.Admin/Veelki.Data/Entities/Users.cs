using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Veelki.Data.Entities
{
    public partial class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }        
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }     
        public bool RollingCommission { get; set; }
        public long AssignCoin { get; set; }
        public double Commision { get; set; }
        public long ExposureLimit { get; set; }
        public int ParentId { get; set; }
        public int Status { get; set; } // 1 for active. 2 for inactive. 3 for blocked
        public string City { get; set; }
        public string State { get; set; }
    }
}
