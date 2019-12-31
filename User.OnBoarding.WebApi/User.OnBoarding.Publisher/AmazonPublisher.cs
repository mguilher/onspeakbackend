using Amazon.SQS;
using System;
using System.Threading.Tasks;
using User.OnBoarding.Publisher.ExtensionMethod;
using User.OnBoarding.ValueObject;

namespace User.OnBoarding.Publisher
{
    public interface IAmazonPublisher
    {
        Task Execute(UserSignUpValue user);
    }

    public class AmazonPublisher : IAmazonPublisher
    {
        private readonly IAmazonSQS _sqs;
        private readonly IPublisherConfiguration _config;

        public AmazonPublisher(IAmazonSQS sqs, IPublisherConfiguration config)
        {
            _sqs = sqs;
            _config = config;
        }

        public async Task Execute(UserSignUpValue user)
        {
            var sendMessageResponse = await _sqs.SendMessageAsync(user.ToUserSignUpValue(_config));
        }

    }
}
