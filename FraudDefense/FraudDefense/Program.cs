using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using FraudDefense.Application;
using FraudDefense.Application.Publish;
using FraudDefense.Application.Validation;
using FraudDefense.Gateway;
using FraudDefense.Gateway.IpQualityScore;
using FraudDefense.Gateway.Serpro;
using FraudDefense.Processor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FraudDefense
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSystemd()
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            })
            .ConfigureServices((hostContext, services) =>
            {
                var options = hostContext.Configuration.GetAWSOptions();
                services.AddDefaultAWSOptions(options);
                services.AddAWSService<IAmazonSQS>();
                services.AddAWSService<IAmazonSimpleNotificationService>();

                services.AddHostedService<Worker>();

                services.AddSingleton<ISQSConfiguration, SQSConfiguration>();
                services.AddSingleton<IIpQualityScoreConfiguration, IpQualityScoreConfiguration>();
                services.AddSingleton<ISerproConfiguration, SerproConfiguration>();

                services.AddTransient<IIpQualityScoreProxy, IpQualityScoreProxy>();
                services.AddTransient<ISerproProxy, SerproProxy>();

                services.AddSingleton<IValidationIterator, ValidationIterator>();

                services.AddTransient<CpfValidation>();
                services.AddTransient<EmailValidation>();

                services.AddTransient<IPublishConfiguration, PublishConfiguration>();
                services.AddTransient<IFraudDefensePublish, FraudDefensePublish>();

                services.AddTransient<ValidationResolver>(serviceProvider => key =>
                {
                    switch (key)
                    {
                        case ValidationType.Cpf:
                            return serviceProvider.GetService<CpfValidation>();
                        case ValidationType.Email:
                            return serviceProvider.GetService<EmailValidation>();
                        default:
                            return null;
                    }
                });

            });
    }
}
