using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.OnBoarding.Application.ExtensionMethod;
using User.OnBoarding.Repository;

namespace User.OnBoarding.Application
{
    public interface IUserOnBoardingService
    {
        Task ShouldEnableAccount(ReceiveMessageRequest messageRequest);
    }

    public class UserOnBoardingService : IUserOnBoardingService
    {
        private readonly IAmazonSQS _sqs;
        private readonly IUserRepository _repository;
        private readonly ISendEmail _email;

        public UserOnBoardingService(IAmazonSQS sqs, IUserRepository repository, ISendEmail email)
        {
            _sqs = sqs;
            _repository = repository;
            _email = email;
        }

        public async Task ShouldEnableAccount(ReceiveMessageRequest messageRequest)
        {
            var response = await _sqs.ReceiveMessageAsync(messageRequest);
            foreach (var message in response.Messages)
            {
                var awsResponse = message.Body.ToAWSUserResponse();
                _email.Execute(awsResponse.EverythingIsFine, awsResponse.Email);
                _repository.UpdateUserStatus(awsResponse.UserId, awsResponse.EverythingIsFine);
                await _sqs.DeleteMessageAsync(messageRequest.QueueUrl, message.ReceiptHandle);
            }
        }

    }
}
