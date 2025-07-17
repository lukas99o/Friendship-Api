using MailKit.Net.Smtp;
using MimeKit;

namespace Vänskap_Api.Service
{
    public class EmailService : IService.IEmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("friendshipandevents@outlook.com"));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart("html")
            {
                Text = body
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.outlook.com", 587, false);
            await smtp.AuthenticateAsync("friendshipandevents@outlook.com", "ltjoszwdmumdtrin");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
