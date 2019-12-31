using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDefense.Application.Publish.Model
{
    public class MessageToPublish
    {
        private AWSMessageResponse.SqsResponse _response;
        public MessageToPublish(AWSMessageResponse.SqsResponse response, bool everythingIsFine)
        {
            _response = response;
            EverythingIsFine = everythingIsFine;
        }

        public Guid UserId => _response.UserId;
        public string EmailAddress => _response.EmailAddress;
        public bool EverythingIsFine { get; private set; }
    }
}
