using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDefense.Gateway.Serpro.Contracts
{
    public class CpfInfoResponse
    {
        public string Ni { get; set; }
        public string Nome { get; set; }
        public SituacaoCadastralResponse Situacao { get; set; }
    }

    public class SituacaoCadastralResponse
    {
        public SituacaoCadastral Codigo { get; set; }
    }

    public enum SituacaoCadastral
    {
        DefaultValue = -1,

        Regular = 0,
        Suspensa = 2,
        TitularFalecido = 3,
        PendenteRegularizacao = 4,
        CanceladaMultiplicidade = 5,
        Nula = 8,
        CanceladaOficio = 9,

        InformacoesIncompletas = 206,
        CpfInvalido = 400,
        CpfInexistente = 404,

    }
}
