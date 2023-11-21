using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace SendingEmail
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void SenEmail(EmailDto request)
        {
            var host = _config.GetSection("EmailHost").Value;
            var EmailUsername = _config.GetSection("EmailUsername").Value;
            var EmailPassword = _config.GetSection("EmailPassword").Value;
            var port = _config.GetSection("port").Value;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(EmailUsername));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect( host, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(EmailUsername, EmailPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
