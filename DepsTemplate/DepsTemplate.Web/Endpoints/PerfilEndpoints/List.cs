using Ardalis.ApiEndpoints;
using DepsTemplate.Core.Entities.PerfilAggregate;
using DepsTemplate.Core.Entities.PerfilAggregate.Specifications;
using DepsTemplate.Core.Enums;
using DepsTemplate.SharedKernel.Interfaces;
using DepsTemplate.Web.Base;
using DepsTemplate.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepsTemplate.Web.Endpoints.PerfilEndpoints
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [AllowAnonymous]
    public class List : EndpointBaseAsync
        .WithRequest<ListPerfilRequest>
        .WithActionResult<PagedResponse<ListPerfilResponse>>
    {
        private readonly IReadRepository<Perfil> _repository;

        public List(IReadRepository<Perfil> repository)
        {
            _repository = repository;
        }

        [HttpGet(ListPerfilRequest.Route)]
        [SwaggerOperation(
          Summary = "Obtém uma lista de perfis filtrados por ativo e nome",
          Description = "Obtém uma lista de perfis filtrados por ativo e nome",
          OperationId = "Perfil.List",
          Tags = new[] { "PerfilEndpoints" })
        ]
        public override async Task<ActionResult<PagedResponse<ListPerfilResponse>>> HandleAsync([FromQuery] ListPerfilRequest request, CancellationToken cancellationToken = default)
        {
            var spec = new PerfisPorAtivoSpec(request.Ativo, request.Filter, request.Page, request.Size);
            var total = await _repository.CountAsync(spec, cancellationToken);
            var result = await _repository.ListAsync(spec, cancellationToken);
            var response = new PagedResponse<ListPerfilResponse>
            {
                TotalItems = total,
                Items = result.Select(x => new ListPerfilResponse
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Ativo = x.Ativo                    
                }).ToList()
            };
            return Ok(response);
        }
    }
}
