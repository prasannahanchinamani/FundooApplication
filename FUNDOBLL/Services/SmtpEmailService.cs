using BusinessLogicLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
   public class SmtpEmailService:IEmailService
    {
        private readonly IConfiguration configuration;

        public SmtpEmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Send(string to, string subject, string body)
        {
            var fromEmail = configuration["EmailSettings:FromEmail"];
            var password = configuration["EmailSettings:Password"];
            var host=configuration["EmailSettings:Host"];
            var port = int.Parse(configuration["EmailSettings:Port"]);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail, "FunDoo App");
            mail.To.Add(to);
            mail.Subject= subject;
            mail.Body= body;
            SmtpClient smtp = new SmtpClient(host, port);
            smtp.Credentials = new NetworkCredential(fromEmail, password);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
