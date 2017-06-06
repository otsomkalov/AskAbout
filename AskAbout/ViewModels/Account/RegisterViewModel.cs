using System.ComponentModel.DataAnnotations;

namespace AskAbout.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "EmptyLog",
            ErrorMessageResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel))]
        [StringLength(20, ErrorMessageResourceName = "LogPass",
            ErrorMessageResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel), MinimumLength = 5)]
        public string Login { get; set; }

        [Required(ErrorMessageResourceName = "EmptyEmail",
            ErrorMessageResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel))]
        [EmailAddress(ErrorMessageResourceName = "EmailErr",
            ErrorMessageResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "EmptyPass",
            ErrorMessageResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel))]
        [StringLength(100, ErrorMessageResourceName = "LogPass",
            ErrorMessageResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "Match",
            ErrorMessageResourceType = typeof(Resources.ViewModels.Account.RegisterViewModel))]
        public string ConfirmPassword { get; set; }
    }
}