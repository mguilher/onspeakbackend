using Amazon.SQS;
using Amazon.SQS.Model;
using FraudDefense.Application.ExtensionMethod;
using FraudDefense.Application.Publish;
using FraudDefense.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDefense.Application
{
    public interface IValidationIterator
    {
        Task<bool> Run(ReceiveMessageRequest sqsRequest);
    }

    public class ValidationIterator : IValidationIterator
    {
        private readonly ValidationResolver _resolver;
        private readonly IAmazonSQS _sqs;
        private readonly IFraudDefensePublish _publisher;

        public ValidationIterator(ValidationResolver resolver, IAmazonSQS sqs,
                                 IFraudDefensePublish publisher)
        {
            _resolver = resolver;
            _sqs = sqs;
            _publisher = publisher;
        }

        public async Task<bool> Run(ReceiveMessageRequest sqsRequest)
        {
            ReceiveMessageResponse sqsResponse = await _sqs.ReceiveMessageAsync(sqsRequest);
            if (!sqsResponse.Messages.Any())
                return true;

            return await ExcuteValidator(sqsResponse, sqsRequest);
        }

        private async Task<bool> ExcuteValidator(ReceiveMessageResponse sqsResponse, ReceiveMessageRequest sqsRequest)
        {
            var everythingIsFine = true;
            var messageSqsResponse = sqsResponse.Messages.First();

            var message =  messageSqsResponse.Body.ToSqsResponse();
            var validators = CreateValidators();
            foreach (var item in validators)
                if (!await item.IsValidAsync(message))
                {
                    everythingIsFine = false;
                    break;
                }


            await _publisher.Action(message, everythingIsFine);
            await _sqs.DeleteMessageAsync(sqsRequest.QueueUrl, messageSqsResponse.ReceiptHandle);

            return true;
        }

        private IList<IValidation> CreateValidators()
            =>
            new List<IValidation>
            {
                {_resolver(ValidationType.Email) },
                {_resolver(ValidationType.Cpf) },
            };
        
    }
}
