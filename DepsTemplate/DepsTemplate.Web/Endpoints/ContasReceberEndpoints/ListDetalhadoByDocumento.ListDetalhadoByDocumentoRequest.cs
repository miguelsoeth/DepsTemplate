using DepsTemplate.Core.Enums;
using DepsTemplate.Web.Base;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DepsTemplate.Web.Endpoints.ContasReceberEndpoints
{
    public class ListDetalhadoByDocumentoRequest : PagedRequest
    {
        public const string Route = "contas-receber/{Documento}/detalhes";
        public static string BuildRoute(string documento) => Route.Replace("{Documento}", documento);

        public Guid ClienteId { get; set; }

        [FromRoute]
        public string Documento { get; set; }

        [FromQuery]
        public TipoContaReceber Tipo { get; set; }
    }
}
