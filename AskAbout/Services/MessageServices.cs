using System.Threading.Tasks;
using AskAbout.Services.Interfaces;

namespace AskAbout.Services
{
    public class AuthMessageSender : IEmailSender
    {
        public async Task SendEmailAsync(string author, string email, string subject, string message)
        {
//            var emailMessage = new MimeMessage();
//
//            emailMessage.From.Add(new MailboxAddress(author, "notidicationsender@gmail.com"));
//            emailMessage.To.Add(new MailboxAddress("", email));
//            emailMessage.Subject = subject;
//            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
//            {
//                Text = message
//            };
//
//            using (var client = new SmtpClient())
//            {
//                await client.ConnectAsync("smtp.gmail.com", 465, true);
//                await client.AuthenticateAsync("notidicationsender@gmail.com", "we34n8xza");
//                await client.SendAsync(emailMessage);
//
//                await client.DisconnectAsync(true);
//            }
            return;
        }
    }
}