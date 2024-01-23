using Ardalis.ApiEndpoints;
using DepsTemplate.Core.Entities.PerfilAggregate;
using DepsTemplate.Core.Entities.PerfilAggregate.Specifications;
using DepsTemplate.Core.Enums;
using DepsTemplate.SharedKernel.Interfaces;
using DepsTemplate.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepsTemplate.Web.Endpoints.PerfilEndpoints
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [AllowAnonymous]
    public class GetAllWithOrder : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<GetAllPerfilWithOrderResponse>>
    {
        private readonly IReadRepository<Perfil> _repository;

        public GetAllWithOrder(IReadRepository<Perfil> repository)
        {
            _repository = repository;
        }

        [HttpGet("perfil/ordenados")]
        [SwaggerOperation(
          Summary = "Obtém uma lista de perfis filtrados por ativo ordenados",
          OperationId = "Perfil.GetAllWithOrder",
          Tags = new[] { "PerfilEndpoints" })
        ]
        public override async Task<ActionResult<List<GetAllPerfilWithOrderResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var spec = new PerfisOrdenadosSpec();
            var perfis = await _repository.ListAsync(spec, cancellationToken);

            return Ok(perfis.Select(x => new GetAllPerfilWithOrderResponse
            {
                Id = x.Id,
                Nome = x.Nome,
                Ordem = x.Ordem
            }).ToList());
        }
    }
}
