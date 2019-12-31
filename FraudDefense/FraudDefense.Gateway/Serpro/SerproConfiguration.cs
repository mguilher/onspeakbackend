using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDefense.Gateway.Serpro
{
    public interface ISerproConfiguration
    {
        string ServiceUrl { get; }
        string CpfEndPoint { get; }
        string ApiKey { get; }
    }
    public class SerproConfiguration : ISerproConfiguration
    {
        private readonly IConfiguration _configuration;

        public SerproConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            PrepareSettings();
        }

        public string ServiceUrl { get; set; }
        public string CpfEndPoint { get; set; }
        public string ApiKey { get; set; }

        private void PrepareSettings()
        {
            ServiceUrl = _configuration["Serpro:ServiceUrl"];
            CpfEndPoint = _configuration["Serpro:CpfEndPoint"];
            ApiKey = _configuration["Serpro:ApiKey"];
        }
    }
}
