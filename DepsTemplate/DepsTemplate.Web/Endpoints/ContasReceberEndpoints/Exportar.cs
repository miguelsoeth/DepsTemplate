using Ardalis.ApiEndpoints;
using DepsTemplate.Core.Enums;
using DepsTemplate.Core.Interfaces;
using DepsTemplate.Web.Filters;
using DepsTemplate.Web.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DepsTemplate.Web.Endpoints.ContasReceberEndpoints
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [AllowAnonymous]
    public class Exportar : EndpointBaseSync
        .WithRequest<ExportarRequest>
        .WithActionResult
    {
        private readonly IContasReceberExportarService _contasReceberExportarService;

        public Exportar(IContasReceberExportarService contasReceberExportarService)
        {
            _contasReceberExportarService = contasReceberExportarService;
        }

        [HttpGet(ExportarRequest.Route)]
        [SwaggerOperation(
          Summary = "Exporta o contas a receber para uma planilha",
          Description = "Exporta o contas a receber para uma planilha",
          OperationId = "ContasReceber.Exportar",
          Tags = new[] { "ContasReceberEndpoints" })
        ]
        public override ActionResult Handle([FromQuery] ExportarRequest request)
        {
            if (User.GetPerfilUsuario() == PerfilUsuario.UsuarioGestor)
            {
                request.ClienteId = User.GetClienteId();
            }

            var stream = _contasReceberExportarService.GerarArquivoContasReceber(request.ClienteId, request.Documento, request.Filter);

            if (stream == null)
            {
                return Ok();
            }

            Response.Headers.Add("Content-Disposition", "attachment");

            return File(stream, "application/vnd.ms-excel");
        }
    }
}
