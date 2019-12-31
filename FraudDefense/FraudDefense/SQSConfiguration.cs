using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDefense.Processor
{
    public  interface ISQSConfiguration
    {
        string Validation { get; }
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

        public string Validation { get; set; }
        public int ReceiveMessagesWaitSeconds { get; set; }

        public int ProcessDelayMilliseconds { get; set; }

        private void PrepareSettings()
        {
            Validation = _configuration["FraudValidation:ValidationSQS"];
            ProcessDelayMilliseconds = Convert.ToInt32(_configuration["FraudValidation:ProcessDelayMilliseconds"]);
        }
    }
}
