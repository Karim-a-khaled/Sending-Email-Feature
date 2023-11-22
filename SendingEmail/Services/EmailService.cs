using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SendingEmail.Models;
using SendingEmail.Models.DTOs;

namespace SendingEmail.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.EmailUserName));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.EmailHost, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.EmailUserName, _emailSettings.EmailPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}