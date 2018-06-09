using BasketWebUI.Interfaces;
using System.Threading.Tasks;

namespace BasketWebUI.Infrastructure.Services
{
    // This class will be used used by the application 
    // to send email for account confirmation and password reset.
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // TODO: Wire this up to actual email sending logic.
            return Task.CompletedTask;
        }
    }
}