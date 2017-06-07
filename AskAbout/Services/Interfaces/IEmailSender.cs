using System.Threading.Tasks;

namespace AskAbout.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string author, string email, string subject, string message);
    }
}