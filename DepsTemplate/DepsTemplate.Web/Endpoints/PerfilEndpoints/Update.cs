using Ardalis.ApiEndpoints;
using DepsTemplate.Core.Enums;
using DepsTemplate.Core.Interfaces;
using DepsTemplate.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace DepsTemplate.Web.Endpoints.PerfilEndpoints
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [AllowAnonymous]
    public class Update : EndpointBaseAsync
        .WithRequest<UpdatePerfilRequest>
        .WithActionResult<UpdatePerfilResponse>
    {
        private readonly IPerfilService _perfilService;

        public Update(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpPut(UpdatePerfilRequest.Route)]
        [SwaggerOperation(
          Summary = "Atualiza perfil",
          OperationId = "Perfil.Update",
          Tags = new[] { "PerfilEndpoints" })
        ]
        public override async Task<ActionResult<UpdatePerfilResponse>> HandleAsync(UpdatePerfilRequest request, CancellationToken cancellationToken = default)
        {
            var metrica = await _perfilService.UpdatePerfilAsync(request.Id, request.Nome, request.Ativo);

            return Ok(new UpdatePerfilResponse
            {
                Id = metrica.Id,
                Nome = metrica.Nome,
                Ativo = metrica.Ativo
            });
        }
    }
}
