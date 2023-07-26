using MiniProject319.ViewModels;

namespace MiniProject319.api.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailSend request);
    }
}
