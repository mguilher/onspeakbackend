using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using FraudDefense.Application.ExtensionMethod;

namespace FraudDefense.Application.Publish
{
    public interface IFraudDefensePublish
    {
        Task Action(AWSMessageResponse.SqsResponse messageToPublish, bool everythinIsFine);
    }
    public class FraudDefensePublish : IFraudDefensePublish
    {

        private readonly IAmazonSimpleNotificationService _snsClient;
        private readonly IPublishConfiguration _publishConfiguration;

        public FraudDefensePublish(IAmazonSimpleNotificationService snsClient, IPublishConfiguration publishConfiguration)
        {
            _snsClient = snsClient;
            _publishConfiguration = publishConfiguration;
        }

        public async Task Action(AWSMessageResponse.SqsResponse messageToPublish, bool everythinIsFine)
        {
            var message = messageToPublish.ToMessageToPublish(everythinIsFine);
            var request = new PublishRequest(_publishConfiguration.UserOnBoardingAccount, message.ToJson());
            await _snsClient.PublishAsync(request);
        }
    }
}
