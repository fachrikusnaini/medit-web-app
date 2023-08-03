using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MiniProject319.api.Models;

using System.Net.Mail;
using System.Configuration;

namespace MiniProject319.api.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public void SendEmail(EmailDto request)
        {
            var emailVerif = new MimeMessage();
            emailVerif.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
            emailVerif.To.Add(MailboxAddress.Parse(request.To));
            emailVerif.Subject = request.Subject;
            emailVerif.Body = new TextPart(TextFormat.Html)
            {
                Text = @"This your Code OTP : "
            };

            //using var smtp = new SmtpClient();
            //smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
            //smtp.Send(emailVerif);
            //smtp.Disconnect(true);
        }
    }
}
