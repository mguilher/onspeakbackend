using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using User.OnBoarding.Application;
using User.OnBoarding.Processor.ExtensionMethod;

namespace User.OnBoarding.Processor.Tasks
{
    public class OnBoardingAccountEnableTask : BackgroundService
    {
        private readonly ILogger<OnBoardingAccountEnableTask> _logger;
        private readonly ISQSConfiguration _sqsConfig;
        private readonly IUserOnBoardingService _service;

        public OnBoardingAccountEnableTask(ILogger<OnBoardingAccountEnableTask> logger,
                     ISQSConfiguration sqsConfig, IUserOnBoardingService service)
        {
            _logger = logger;
            _sqsConfig = sqsConfig;
            _service = service;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var request = _sqsConfig.ToOnBoardingAccountRequest();
                try
                {
                    await _service.ShouldEnableAccount(request);
                    await Task.Delay(_sqsConfig.ProcessDelayMilliseconds);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "OnBoardingAccountEnable Error");
                }
            }
        }
    }
}
