using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "EmptyLog", ErrorMessageResourceType = typeof(Resources.RegisterViewModel))]
        [StringLength(20, ErrorMessageResourceName = "LogPass", ErrorMessageResourceType = typeof(Resources.RegisterViewModel), MinimumLength = 5)]
        public string Login { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required(ErrorMessageResourceName = "EmptyPass", ErrorMessageResourceType = typeof(Resources.RegisterViewModel))]
        [StringLength(100, ErrorMessageResourceName = "LogPass", ErrorMessageResourceType = typeof(Resources.RegisterViewModel), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
