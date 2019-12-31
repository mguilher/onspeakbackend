using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDefense.Application.ExtensionMethod
{
    public static class FraudDefenceExt
    {
        public static AWSMessageResponse.SqsResponse ToSqsResponse(this string json)
            => JsonConvert.DeserializeObject<AWSMessageResponse.SqsResponse>(json);

        public static JsonSerializerSettings Get()
        {
            var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            settings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return settings;
        }

        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value, Get());
        }

        public static Publish.Model.MessageToPublish ToMessageToPublish(this AWSMessageResponse.SqsResponse response, bool everythingIsFine)
            => new Publish.Model.MessageToPublish(response, everythingIsFine);
    }
}
