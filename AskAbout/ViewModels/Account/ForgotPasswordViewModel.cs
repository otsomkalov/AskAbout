using System.ComponentModel.DataAnnotations;

namespace AskAbout.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}