using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using User.OnBoarding.Application;
using User.OnBoarding.Processor.Tasks;
using User.OnBoarding.Repository;

namespace User.OnBoarding.Processor
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

                services.AddHostedService<OnBoardingAccountEnableTask>();


                services.AddTransient<IUserOnBoardingService, UserOnBoardingService>();
                services.AddTransient<ISQSConfiguration, SQSConfiguration>();
                services.AddTransient<IRepositoryConfiguration, RepositoryConfiguration>();

                services.AddScoped<IUserRepository, UserRepository>();

                services.AddScoped<IEmailConfiguration, EmailConfiguration>();
                services.AddScoped<ISendEmail, SendEmail>();

            });
    }
}
