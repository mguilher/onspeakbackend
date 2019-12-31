using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDefense.Application.AWSMessageResponse
{
    public class SqsResponse
    {
        public Guid UserId { get; set; }
        public string EmailAddress { get; set; }
        public string UserDocument { get; set; }
        public string Name { get; set; }
    }
}
