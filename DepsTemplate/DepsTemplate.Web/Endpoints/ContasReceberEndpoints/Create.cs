using Ardalis.ApiEndpoints;
using DepsTemplate.Core.DTO;
using DepsTemplate.Core.Enums;
using DepsTemplate.Core.Interfaces;
using DepsTemplate.Web.Filters;
using DepsTemplate.Web.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepsTemplate.Web.Endpoints.ContasReceberEndpoints
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [AllowAnonymous]
    public class Create : EndpointBaseAsync
    .WithRequest<CreateContasReceberRequest>
    .WithActionResult
    {
        private readonly IContasReceberStorageService _contasReceberStorageService;

        public Create(IContasReceberStorageService contasReceberStorageService)
        {
            _contasReceberStorageService = contasReceberStorageService;
        }

        [HttpPost(CreateContasReceberRequest.Route)]
        [SwaggerOperation(
            Summary = "Integração de contas à receber",
            OperationId = "ContasReceber.Create",
            Tags = new[] { "ContasReceberEndpoints" })
        ]
        public override async Task<ActionResult> HandleAsync(CreateContasReceberRequest request, CancellationToken cancellationToken = default)
        {
            var usuarioIdString = User.GetUsuarioId().ToString();
            var clienteIdString = User.GetClienteId().ToString();

            var contasReceberDto = request.ContasReceber.Select(x => new ContasReceberDto
            {
                Data = DateTime.Now,
                ClienteId = clienteIdString,
                UsuarioId = usuarioIdString,
                Documento = x.Documento,
                Dados = x.Dados
            }).ToList();

            await _contasReceberStorageService.SalvarCr(contasReceberDto);

            return Ok();
        }
    }
}
