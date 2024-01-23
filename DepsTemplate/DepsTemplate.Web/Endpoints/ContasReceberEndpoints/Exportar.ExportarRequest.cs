using Microsoft.AspNetCore.Mvc;
using System;

namespace DepsTemplate.Web.Endpoints.ContasReceberEndpoints
{
    public class ExportarRequest
    {
        public const string Route = "contas-receber/exportar";

        public Guid ClienteId { get; set; }

        [FromQuery]
        public string Documento { get; set; }

        [FromQuery]
        public string Filter { get; set; }
    }
}