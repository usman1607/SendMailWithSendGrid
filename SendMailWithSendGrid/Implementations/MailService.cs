using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendMailWithSendGrid.Interfaces;
using SendMailWithSendGrid.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

            var attachments = new List<Attachment>();
            foreach (var attach in request.Attachments)
            {
                if (attach != null && attach.Length > 0)
                {
                    string data = string.Empty;
                    var fileName = attach.ContentType.Split('/')[0];
                    string contentType = attach.ContentType.Split('/')[1];

                    using (var fs = new MemoryStream())
                    {
                        attach.CopyTo(fs);
                        var fileBytes = fs.ToArray();
                        data = Convert.ToBase64String(fileBytes);
                    }

                    var attachment = new Attachment()
                    {
                        Content = data,
                        Type = contentType,
                        Filename = $"{fileName}.{contentType}",
                        Disposition = "inline",
                        ContentId = "Banner"
                    };
                    attachments.Add(attachment);
                }
            }
            msg.AddAttachments(attachments);

            var response = await client.SendEmailAsync(msg);

            return response;
        }
    }
}
