using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendMailWithSendGrid.Models
{
    public class MailRequest
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string RecieverEmail { get; set; }
        public string RecieverName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
    }
}
