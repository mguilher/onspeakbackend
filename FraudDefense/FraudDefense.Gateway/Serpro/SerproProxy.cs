using Flurl;
using Flurl.Http;
using FraudDefense.Gateway.Serpro.Contracts;
using FraudDefense.Gateway.Serpro.Exception;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FraudDefense.Gateway.Serpro
{
    public interface ISerproProxy
    {
        Task<CpfInfoResponse> CpfInfo(string cpf);
    }

    public class SerproProxy : ISerproProxy
    {
        private ISerproConfiguration _config;

        public SerproProxy(ISerproConfiguration config)
        {
            _config = config;
        }

        public async Task<CpfInfoResponse> CpfInfo(string cpf)
        {
            var responseMessage = await _config.ServiceUrl.AppendPathSegment(_config.CpfEndPoint)
                .AppendPathSegment(cpf)
                .WithOAuthBearerToken(_config.ApiKey)
                .WithHeader("Content-Type", "application/json")
                .AllowHttpStatus("4xx")
                .AllowHttpStatus("5xx")
                .GetAsync();

            var situacaoCpf = await HandleErrorIfInvalid(responseMessage);
            if (situacaoCpf == SituacaoCadastral.DefaultValue)
                return await Task.FromResult(responseMessage).ReceiveJson<CpfInfoResponse>();
            return new CpfInfoResponse { Situacao = new SituacaoCadastralResponse { Codigo = situacaoCpf } };
        }

        private async Task<SituacaoCadastral> HandleErrorIfInvalid(HttpResponseMessage executeHttp)
        {
            if (executeHttp.IsSuccessStatusCode)
                return SituacaoCadastral.DefaultValue;

            return await VerifyHttpStatusCode(executeHttp);
        }

        private async Task<SituacaoCadastral> VerifyHttpStatusCode(HttpResponseMessage executeHttp)
        {
            switch (executeHttp.StatusCode)
            {
                case HttpStatusCode.PartialContent:
                    return SituacaoCadastral.InformacoesIncompletas;
                case HttpStatusCode.BadRequest:
                    return SituacaoCadastral.CpfInvalido;
                case HttpStatusCode.NotFound:
                    return SituacaoCadastral.CpfInexistente;
                case HttpStatusCode.InternalServerError:
                    {
                        var serviceException = await Task.FromResult(executeHttp).ReceiveString();
                        throw new SerproException(executeHttp.StatusCode, serviceException);
                    }
                default:
                    return SituacaoCadastral.DefaultValue;
            }
        }
    }

}
