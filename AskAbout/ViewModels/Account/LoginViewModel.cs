using System.ComponentModel.DataAnnotations;

namespace AskAbout.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "EmptyLog",
            ErrorMessageResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel))]
        [StringLength(20, ErrorMessageResourceName = "LogPass",
            ErrorMessageResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel), MinimumLength = 5)]
        public string Login { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required(ErrorMessageResourceName = "EmptyPass",
            ErrorMessageResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel))]
        [StringLength(100, ErrorMessageResourceName = "LogPass",
            ErrorMessageResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}