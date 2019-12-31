using Microsoft.Extensions.Configuration;
using System;

namespace User.OnBoarding.Application
{
    public interface IEmailConfiguration
    {
        string Smtp { get; }
        int Port { get; }
        bool Ssl { get; }
        string Email { get; }
        string Password { get; }
    }
    public class EmailConfiguration : IEmailConfiguration
    {
        private readonly IConfiguration _config;

        public EmailConfiguration(IConfiguration config)
        {
            _config = config;
            Smtp = _config["Email:EmailSmtp"]; 
            Port = Convert.ToInt32( _config["Email:EmailPort"]); 
            Ssl = Convert.ToBoolean( _config["Email:Ssl"]); 
            Email = _config["Email:EmailAddress"]; 
            Password = _config["Email:Password"]; 
        }

        public string Smtp { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
