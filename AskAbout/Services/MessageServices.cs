using System.Threading.Tasks;
using AskAbout.Services.Interfaces;

namespace AskAbout.Services
{
    public class AuthMessageSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}