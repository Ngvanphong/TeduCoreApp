using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class SendMailService : ISendMailService
    {
        private IConfiguration _config;
        public SendMailService(IConfiguration config)
        {
            _config = config;
        }
        public Task SendMultiEmail(List<string> listEmail, string subject, string message,string token)
        {
            SmtpClient client = new SmtpClient(_config["MailSettings:Server"])
            {
                UseDefaultCredentials = false,
                Port = int.Parse(_config["MailSettings:Port"]),
                EnableSsl = bool.Parse(_config["MailSettings:EnableSsl"]),
                Credentials = new NetworkCredential(_config["MailSettings:UserName"], _config["MailSettings:Password"])
            };
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_config["MailSettings:FromEmail"], _config["MailSettings:FromName"]),
            };
           
            mailMessage.Body = message;
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            foreach (var email in listEmail)
            {
                mailMessage.Bcc.Add(email);                           
            }
            client.SendAsync(mailMessage, token);
            return Task.CompletedTask;
        }
    }
    
}
