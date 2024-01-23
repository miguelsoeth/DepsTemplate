using DepsTemplate.Web.Base;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DepsTemplate.Web.Endpoints.ContasReceberEndpoints
{
    public class ListByDocumentoRequest : PagedRequest
    {
        public const string Route = "contas-receber";

        public Guid ClienteId { get; set; }

        [FromQuery]
        public string Documento { get; set; }

        [FromQuery]
        public string Filter { get; set; }
    }
}
