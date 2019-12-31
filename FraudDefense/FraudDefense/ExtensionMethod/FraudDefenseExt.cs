using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDefense.Processor.ExtensionMethod
{
    public static class FraudDefenseExt
    {
        public static ReceiveMessageRequest ToReceiveMessageRequest(this ISQSConfiguration sqsConfig, int waitTimeSeconds = 5)
        => new ReceiveMessageRequest
        {
            QueueUrl = sqsConfig.Validation,
            WaitTimeSeconds = waitTimeSeconds,
            MaxNumberOfMessages = 1,
        };
    }
}
