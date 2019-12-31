using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace User.OnBoarding.Application
{
    public interface ISendEmail
    {
        void Execute(bool approval, string userEmail);
    }
    public class SendEmail : ISendEmail
    {
        public SendEmail(IEmailConfiguration config)
        {
            _config = config;
        }

        private readonly IEmailConfiguration _config;

        public void Execute(bool approval, string userEmail)
        {
            SmtpClient SmtpServer = new SmtpClient(_config.Smtp);
            SmtpServer.Port = _config.Port;
            SmtpServer.Credentials = new NetworkCredential(_config.Email, _config.Password);
            SmtpServer.EnableSsl = _config.Ssl;

            var mail = new MailMessage();
            mail.From = new MailAddress(_config.Email);
            mail.To.Add(userEmail);
            mail.Subject = "OnSpeak";
            mail.Body = GetMessage(approval);

            SmtpServer.Send(mail);
        }

        private string GetMessage(bool approval)
        => approval ? "Welcome. Enjoy the Journey on Speak."
                : "Unfortunately, your account has not been approved.";
        
    }


}
