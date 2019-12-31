using Amazon.SQS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.OnBoarding.Processor.ExtensionMethod
{
    public static class UserOnBoardingExt
    {
        public static ReceiveMessageRequest ToOnBoardingAccountRequest(this ISQSConfiguration sqsConfig, int waitTimeSeconds = 5)
        => new ReceiveMessageRequest
        {
            QueueUrl = sqsConfig.UserOnBoardingAccount,
            WaitTimeSeconds = waitTimeSeconds,
            MaxNumberOfMessages = 1,
        };

        public static ReceiveMessageRequest ToOnBoardingPremiumRequest(this ISQSConfiguration sqsConfig, int waitTimeSeconds = 5)
        => new ReceiveMessageRequest
        {
            QueueUrl = sqsConfig.UserOnBoardingPremium,
            WaitTimeSeconds = waitTimeSeconds,
            MaxNumberOfMessages = 1,
        };


    }
}
