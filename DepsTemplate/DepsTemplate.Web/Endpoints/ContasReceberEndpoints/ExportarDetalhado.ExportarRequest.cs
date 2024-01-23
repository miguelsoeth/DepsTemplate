using DepsTemplate.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DepsTemplate.Web.Endpoints.ContasReceberEndpoints
{
    public class ExportarDetalhadoRequest
    {
        public const string Route = "contas-receber/{Documento}/detalhes/exportar";
        public static string BuildRoute(string documento) => Route.Replace("{Documento}", documento);

        public Guid ClienteId { get; set; }

        [FromRoute]
        public string Documento { get; set; }

        [FromQuery]
        public string Filter { get; set; }

        [FromQuery]
        public TipoContaReceber Tipo { get; set; }
    }
}