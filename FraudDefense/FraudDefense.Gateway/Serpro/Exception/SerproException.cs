using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FraudDefense.Gateway.Serpro.Exception
{
    class SerproException : ApplicationException
    {
            public string SerproMessage { get; }
            public HttpStatusCode StatusCode { get; set; }

        public SerproException(HttpStatusCode statusCode, string response) : base(response)
        {
            StatusCode = statusCode;
            SerproMessage = response;
        }
    }
}
