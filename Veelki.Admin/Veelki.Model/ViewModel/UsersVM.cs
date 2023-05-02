using System;

namespace Veelki.Model.ViewModel
{
    public class UsersVM
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
        public long AvailableBalance { get; set; }
        public long ProfitAndLoss { get; set; }
        public bool IsAbleToChange { get; set; }
        public string RoleName { get; set; }
    }
}
