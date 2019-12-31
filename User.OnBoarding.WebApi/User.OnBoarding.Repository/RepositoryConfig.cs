using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.OnBoarding.Repository
{
    public interface IRepositoryConfig
    {
        string ConnectionString { get; }
    }

    public class RepositoryConfig : IRepositoryConfig
    {
        public string ConnectionString { get; set; }

        public RepositoryConfig(IConfiguration configuration)
        {
            ConnectionString = configuration["Database:ConnectionString"];
        }

    }
}
