﻿using MailKit.Net.Smtp;
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
            await smtp.ConnectAsync("smtp-relay.brevo.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("92c0e1001@smtp-brevo.com", "VsCD4WUFhqI7xnMH");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}


