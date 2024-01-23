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
    public class ExportarDetalhado : EndpointBaseSync
        .WithRequest<ExportarDetalhadoRequest>
        .WithActionResult
    {
        private readonly IContasReceberExportarDetalhadoService _contasReceberExportarDetalhadoService;

        public ExportarDetalhado(IContasReceberExportarDetalhadoService contasReceberExportarDetalhadoService)
        {
            _contasReceberExportarDetalhadoService = contasReceberExportarDetalhadoService;
        }

        [HttpGet(ExportarDetalhadoRequest.Route)]
        [SwaggerOperation(
          Summary = "Exporta o contas a receber detalhado para uma planilha",
          Description = "Exporta o contas a receber detalhado para uma planilha",
          OperationId = "ContasReceber.ExportarDetalhado",
          Tags = new[] { "ContasReceberEndpoints" })
        ]
        public override ActionResult Handle([FromQuery] ExportarDetalhadoRequest request)
        {
            if (User.GetPerfilUsuario() == PerfilUsuario.UsuarioGestor)
            {
                request.ClienteId = User.GetClienteId();
            }

            var stream = _contasReceberExportarDetalhadoService.GerarArquivoContasReceberDetalhado(request.ClienteId, request.Documento, request.Tipo);

            if (stream == null)
            {
                return Ok();
            }

            Response.Headers.Add("Content-Disposition", "attachment");

            return File(stream, "application/vnd.ms-excel");
        }
    }
}
