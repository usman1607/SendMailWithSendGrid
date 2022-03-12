using Microsoft.AspNetCore.Mvc;
using SendMailWithSendGrid.Interfaces;
using SendMailWithSendGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendMailWithSendGrid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            try
            {
                var response = await mailService.SendEmailAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return Ok(response);
                }
                return Problem(response.ToString());
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
