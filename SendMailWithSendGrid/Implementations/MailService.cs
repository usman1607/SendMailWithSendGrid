using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendMailWithSendGrid.Interfaces;
using SendMailWithSendGrid.Models;
using System.Threading.Tasks;

namespace SendMailWithSendGrid.Implementations
{
    public class MailService : IMailService
    {
        private readonly Settings.MailSettings _mailSettings;
        public MailService(IOptions<Settings.MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<Response> SendEmailAsync(MailRequest request)
        {
            var apiKey = _mailSettings.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(request.SenderEmail, request.SenderName);
            var to = new EmailAddress(request.RecieverEmail, request.RecieverName);
            var plainTextContent = request.Content;
            var htmlContent = $"<strong>{request.Content}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, request.Subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response;
        }
    }
}
