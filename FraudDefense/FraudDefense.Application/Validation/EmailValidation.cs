using Amazon.SQS;
using Amazon.SQS.Model;
using FraudDefense.Application.ExtensionMethod;
using FraudDefense.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDefense.Application.Validation
{
    public class EmailValidation : IValidation
    {
        private readonly IIpQualityScoreProxy _proxy;

        public EmailValidation(IIpQualityScoreProxy proxy)
        {
            _proxy = proxy;
        }

        public async Task<bool> IsValidAsync(AWSMessageResponse.SqsResponse message)
        {
            if (message == null)
                return true;

            try
            {
                var gatewayResponse = await _proxy.EmailInfo(message.EmailAddress);
                if (gatewayResponse.Disposable || gatewayResponse.Recent_abuse ||
                    gatewayResponse.Suspect || gatewayResponse.Honeypot ||
                    gatewayResponse.Deliverability.ToLowerInvariant() == "low" ||
                    (!gatewayResponse.Valid))
                    return false;

                return (gatewayResponse.Overall_score == 4 && gatewayResponse.Smtp_score == 3);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
