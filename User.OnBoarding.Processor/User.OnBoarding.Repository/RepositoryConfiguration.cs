using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.OnBoarding.Repository
{
    public interface IRepositoryConfiguration
    {
        string ConnectionString { get; }
    }

    public class RepositoryConfiguration : IRepositoryConfiguration
    {
        private IConfiguration _configuration;

        public RepositoryConfiguration(IConfiguration config)
        {
            _configuration = config;
            PrepareSettings();
        }

        private void PrepareSettings()
        => ConnectionString = _configuration["Repository:ConnectionString"];

        public string ConnectionString { get; set; }
    }
}
