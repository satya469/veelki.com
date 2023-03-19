using System.ComponentModel.DataAnnotations;

namespace Veelki.Models.ViewModel
{
    public class LoginViewModel
    {        
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }        

        // Because of using _UserLayout page on my profile
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public int Role { get; set; }
        public float AssignCoin { get; set; }
        public float Commision { get; set; }
        public int ExposureLimit { get; set; }
        public string MobileNumber { get; set; }
        public int ValidationCode { get; set; }
        public int TxtValidationCode { get; set; }
    }
}
