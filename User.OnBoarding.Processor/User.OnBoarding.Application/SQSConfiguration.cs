using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.OnBoarding.Processor
{
    public interface ISQSConfiguration
    {
        string UserOnBoardingAccount { get; }
        string UserOnBoardingPremium { get; }
        int ReceiveMessagesWaitSeconds { get; }
        int ProcessDelayMilliseconds { get; }
    }

    public class SQSConfiguration : ISQSConfiguration
    {
        private readonly IConfiguration _configuration;

        public SQSConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            PrepareSettings();
        }

        public string UserOnBoardingAccount { get; set; }
        public string UserOnBoardingPremium { get; set; }
        public int ReceiveMessagesWaitSeconds { get; set; }

        public int ProcessDelayMilliseconds { get; set; }

        private void PrepareSettings()
        {
            UserOnBoardingAccount = _configuration["UserOnBoardingAction:UserOnBoardingAccountSQS"];
            UserOnBoardingPremium = _configuration["UserOnBoardingAction:UserOnBoardingPremiumSQS"];

            ReceiveMessagesWaitSeconds = Convert.ToInt32(_configuration["UserOnBoardingAction:ReceiveMessagesWaitSecondsSQS"]);
            ProcessDelayMilliseconds = Convert.ToInt32(_configuration["UserOnBoardingAction:ProcessDelayMilliseconds"]);
        }
    }
}
