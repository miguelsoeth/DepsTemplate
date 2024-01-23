using DepsTemplate.Core.DTO;
using System.Collections.Generic;

namespace DepsTemplate.Web.Endpoints.ContasReceberEndpoints
{
    public class CreateContasReceberRequest
    {
        public const string Route = "contas-receber";

        public List<ContasReceberRequest> ContasReceber { get; set; }
    }

    public class ContasReceberRequest
    {
        public string Documento { get; set; }
        public DadosComplementaresAnaliseDto Dados { get; set; }
    }
}
