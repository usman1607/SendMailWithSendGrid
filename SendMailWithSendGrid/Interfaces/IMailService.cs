using SendGrid;
using SendMailWithSendGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendMailWithSendGrid.Interfaces
{
    public interface IMailService
    {
        Task<Response> SendEmailAsync(MailRequest request);
    }
}
