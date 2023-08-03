using MiniProject319.api.Models;

namespace MiniProject319.api.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request); 
    }
}
