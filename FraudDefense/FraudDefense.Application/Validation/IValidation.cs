using Amazon.SQS.Model;
using System.Threading.Tasks;

namespace FraudDefense.Application.Validation
{
    public interface IValidation
    {
        Task<bool> IsValidAsync(AWSMessageResponse.SqsResponse message);
    }
}
