using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FraudDefense.Gateway.IpQualityScore.Exception
{
    public class IpQualityScoreException : ApplicationException
    {
        public string IpQualityScoreMessage { get; }
        public HttpStatusCode StatusCode { get; set; }

        public IpQualityScoreException(HttpStatusCode statusCode, string response) : base(response)
        {
            StatusCode = statusCode;
            IpQualityScoreMessage = response;
        }
    }
}
