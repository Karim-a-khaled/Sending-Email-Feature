using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using SendingEmail.Models.DTOs;
using SendingEmail.Services;

namespace SendingEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail(EmailDto request)
        {
            
            _emailService.SendEmail(request);
            return Ok("An Email Was Sent To: " + request.To);
        }
    }
}
