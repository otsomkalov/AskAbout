using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessageResourceName = "LogPass", ErrorMessageResourceType = typeof(Resources.RegisterViewModel), MinimumLength = 5)]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [EmailAddress(ErrorMessageResourceName = "EmailErr", ErrorMessageResourceType = typeof(Resources.RegisterViewModel))]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "LogPass", ErrorMessageResourceType = typeof(Resources.RegisterViewModel), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessageResourceName = "Match", ErrorMessageResourceType = typeof(Resources.RegisterViewModel))]
        public string ConfirmPassword { get; set; }
    }
}
