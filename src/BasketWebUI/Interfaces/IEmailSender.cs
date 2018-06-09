using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketWebUI.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string address, string subject, string body);
    }
}
