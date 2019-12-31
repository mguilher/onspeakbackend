using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FraudDefense.Application;
using FraudDefense.Application.Validation;
using FraudDefense.Processor;
using FraudDefense.Processor.ExtensionMethod;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FraudDefense
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISQSConfiguration _sqsConfig;
        private readonly IValidationIterator  _iterator;

        public Worker(ILogger<Worker> logger, ISQSConfiguration sqsConfig, IValidationIterator iterator)
        {
            _logger = logger;
            _sqsConfig = sqsConfig;
            _iterator = iterator;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var request = _sqsConfig.ToReceiveMessageRequest(_sqsConfig.ReceiveMessagesWaitSeconds);
                try
                {
                     await _iterator.Run(request);
                    await Task.Delay(_sqsConfig.ProcessDelayMilliseconds);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "FraudDefenseWorker Error");
                }
            }
        }
    }
}
