﻿using Veelki.Data.Entities;

namespace Veelki.Model.ViewModel
{
    public class UserProfileVM
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public double Commision { get; set; }
        public bool RollingCommission { get; set; }
        public long ExposureLimit { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public bool AgentRollingCommission { get; set; }
        public bool IsAdmin { get; set; }
        public Users oUser { get; set; }
    }
}
