using Amazon.SQS;
using Amazon.SQS.Model;
using FraudDefense.Application.ExtensionMethod;
using FraudDefense.Gateway.Serpro;
using FraudDefense.Gateway.Serpro.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDefense.Application.Validation
{
    public class CpfValidation : IValidation
    {
        private readonly ISerproProxy _proxy;


        public CpfValidation(ISerproProxy proxy)
        {
            _proxy = proxy;
        }

        public async Task<bool> IsValidAsync(AWSMessageResponse.SqsResponse message)
        {
            if (message == null)
                return true;

            try
            {
                var gatewayResponse = await _proxy.CpfInfo(message.UserDocument);

                var cpfStatus = gatewayResponse.Situacao.Codigo;
                return (cpfStatus == SituacaoCadastral.Regular || cpfStatus == SituacaoCadastral.DefaultValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
