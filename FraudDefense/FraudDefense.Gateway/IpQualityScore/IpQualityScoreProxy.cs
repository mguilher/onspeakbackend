using Flurl;
using Flurl.Http;
using FraudDefense.Gateway.IpQualityScore;
using FraudDefense.Gateway.IpQualityScore.Contracts;
using FraudDefense.Gateway.IpQualityScore.Exception;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FraudDefense.Gateway
{
    public interface IIpQualityScoreProxy
    {
        Task<EmailInfoResponse> EmailInfo(string email);
    }

    public class IpQualityScoreProxy : IIpQualityScoreProxy
    {
        private IIpQualityScoreConfiguration _config;

        public IpQualityScoreProxy(IIpQualityScoreConfiguration config)
        {
            _config = config;
        }

        public async Task<EmailInfoResponse> EmailInfo(string email)
        {
            var responseMessage = await _config.ServiceUrl.AppendPathSegment(_config.EmailEndPoint)
                .AppendPathSegment(_config.ApiKey)
                .AppendPathSegment(email)
                .WithHeader("Content-Type", "application/json")
                .AllowHttpStatus()
                .GetAsync();
            
            await HandleErrorIfInvalid(responseMessage);
            return await Task.FromResult(responseMessage).ReceiveJson<EmailInfoResponse>();
        }

        private async Task HandleErrorIfInvalid(HttpResponseMessage executeHttp)
        {
            if (executeHttp.IsSuccessStatusCode)
                return;

            var serviceException = await Task.FromResult(executeHttp).ReceiveString();
            throw new IpQualityScoreException(executeHttp.StatusCode, serviceException);
        }
    }
}
