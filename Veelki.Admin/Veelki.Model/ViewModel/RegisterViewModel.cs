using Veelki.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RB444.Model.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public int AssignCoin { get; set; }
        public int Commission { get; set; }
        public int ExposureLimit { get; set; }
        //public int Role { get; set; }
        //public List<UserRoles> UserRoles { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        //[Required]
        //[Display(Name = "Username")]
        public string Username { get; set; }
        //public int LoginUserId { get; set; }
        //public int LoginUserRole { get; set; }
        //public List<Users> Users { get; set; }
        //public Users LoginUser { get; set; }
        //public string RoleName { get; set; }
        //public bool RollingCommission { get; set; }
        //public bool IsAbleToChange { get; set; }
    }
}
