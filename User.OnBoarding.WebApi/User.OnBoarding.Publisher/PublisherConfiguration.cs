using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.OnBoarding.Publisher
{
    public interface IPublisherConfiguration
    {
        string FraudDefense { get; }
        string PaymentPlaceOrder { get; }
    }

    public class PublisherConfiguration : IPublisherConfiguration
    {

        public PublisherConfiguration(IConfiguration configuration)
        {
            FraudDefense = configuration["FraudDefense:ValidationSQS"];
            PaymentPlaceOrder = configuration["Payment:PlaceOrderSQS"];
        }

        public string FraudDefense { get; set; }

        public string PaymentPlaceOrder { get; set; }
    }
}
