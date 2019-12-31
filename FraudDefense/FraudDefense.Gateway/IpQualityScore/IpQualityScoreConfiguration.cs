using Microsoft.Extensions.Configuration;

namespace FraudDefense.Gateway.IpQualityScore
{
    public interface IIpQualityScoreConfiguration
    {
        string ServiceUrl { get; }
        string EmailEndPoint { get; }
        string ApiKey { get; }
    }
    public class IpQualityScoreConfiguration : IIpQualityScoreConfiguration
    {
        private readonly IConfiguration _configuration;

        public IpQualityScoreConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            PrepareSettings();
        }

        public string ServiceUrl { get; set; }
        public string EmailEndPoint { get; set; }
        public string ApiKey { get; set; }
        private void PrepareSettings()
        {
            ServiceUrl = _configuration["IpQualityScore:ServiceUrl"];
            EmailEndPoint = _configuration["IpQualityScore:EmailEndPoint"];
            ApiKey = _configuration["IpQualityScore:ApiKey"];
        }
    }
}
