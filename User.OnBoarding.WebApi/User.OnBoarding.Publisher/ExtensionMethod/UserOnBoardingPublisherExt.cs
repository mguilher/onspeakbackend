using Amazon.SQS.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using User.OnBoarding.ValueObject;

namespace User.OnBoarding.Publisher.ExtensionMethod
{
    internal static class UserOnBoardingPublisherExt
    {
        public static SendMessageRequest ToUserSignUpValue(this UserSignUpValue value, IPublisherConfiguration config)
            => new SendMessageRequest
            {
                QueueUrl = config.FraudDefense,
                MessageBody = new { value.UserId, EmailAddress = value.Email, value.UserDocument, Name = value.UserName }.ToJson()
            };

        public static JsonSerializerSettings Get()
        {
            var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            settings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return settings;
        }

        public static string ToJson(this object value)
            =>JsonConvert.SerializeObject(value, Get());

    }
}
