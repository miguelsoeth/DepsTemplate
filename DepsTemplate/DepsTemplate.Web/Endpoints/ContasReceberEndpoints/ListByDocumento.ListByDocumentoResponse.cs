using DepsTemplate.Core.DTO;
using System;

namespace DepsTemplate.Web.Endpoints.ContasReceberEndpoints
{
    public class ListByDocumentoResponse
    {
        public DateTime Data { get; set; }
        public string UsuarioId { get; set; }
        public string ClienteId { get; set; }
        public string Documento { get; set; }
        public DadosComplementaresAnaliseDto Dados { get; set; }
    }
}
