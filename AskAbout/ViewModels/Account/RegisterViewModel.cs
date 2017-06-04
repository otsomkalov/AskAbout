using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "EmptyLog", ErrorMessageResourceType = typeof(Resources.RegisterViewModel))]
        [StringLength(20, ErrorMessageResourceName = "LogPass", ErrorMessageResourceType = typeof(Resources.RegisterViewModel), MinimumLength = 5)]
        public string Login { get; set; }

        [Required(ErrorMessageResourceName = "EmptyEmail", ErrorMessageResourceType = typeof(Resources.RegisterViewModel))]
        [EmailAddress(ErrorMessageResourceName = "EmailErr", ErrorMessageResourceType = typeof(Resources.RegisterViewModel))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "EmptyPass", ErrorMessageResourceType = typeof(Resources.RegisterViewModel))]
        [StringLength(100, ErrorMessageResourceName = "LogPass", ErrorMessageResourceType = typeof(Resources.RegisterViewModel), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "Match", ErrorMessageResourceType = typeof(Resources.RegisterViewModel))]
        public string ConfirmPassword { get; set; }
    }
}
