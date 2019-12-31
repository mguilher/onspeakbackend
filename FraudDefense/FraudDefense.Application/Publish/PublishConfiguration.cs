using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDefense.Application.Publish
{
    public interface IPublishConfiguration
    {
        string UserOnBoardingAccount { get; }
    }

    public class PublishConfiguration : IPublishConfiguration
    {
        private readonly IConfiguration _configuration;

        public PublishConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            PrepareSettings();
        }

        public string UserOnBoardingAccount { get; set; }

        private void PrepareSettings()
        {
            UserOnBoardingAccount = _configuration["PublishInfo:UserOnBoardinAccountSQS"];
        }
    }
}
