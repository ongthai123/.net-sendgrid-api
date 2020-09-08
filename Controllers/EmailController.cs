using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Sending_Email_API.ViewModels;

namespace Sending_Email_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public EmailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [EnableCors("AllowAll")]
        [Route("Send")]
        [HttpPost]
        public async Task<IActionResult> SendEmailAsync([FromBody]SendingEmailViewModel model)
        {
            try
            {
                var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;

                var client = new SendGridClient(apiKey);

                var from = new EmailAddress(model.From, "Jerry Ong");

                List<EmailAddress> tos = new List<EmailAddress>
                {
                    new EmailAddress(model.To, model.FirstName),
                };

                var subject = model.Subject;
                var htmlContent = model.HTMLContent;
                var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, false);
                var response = await client.SendEmailAsync(msg);


                return Ok(new { Message = response.StatusCode });
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}