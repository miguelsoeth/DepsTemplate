using Ardalis.ApiEndpoints;
using DepsTemplate.Core.Entities.PerfilAggregate;
using DepsTemplate.Core.Interfaces;
using DepsTemplate.SharedKernel.Interfaces;
using DepsTemplate.Web.Endpoints.PerfilEndpoints;
using DepsTemplate.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DepsTemplate.Web.Endpoints.PepEndpoints
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [AllowAnonymous]
    public class GetByPeriod : EndpointBaseAsync.WithRequest<GetPepByPeriodRequest>.WithActionResult<GetPepByPeriodResponse>
    {
        private readonly IPepService _pepService;

        public GetByPeriod(IPepService pepService)
        {
            _pepService = pepService;
        }

        [HttpGet(GetPepByPeriodRequest.Route)]
        [SwaggerOperation(
          Summary = "Obtém pep por periodo",
          OperationId = "Pep.GetByPeriod",
          Tags = new[] { "PepEndpoints" })
        ]
        public override async Task<ActionResult<GetPepByPeriodResponse>> HandleAsync(GetPepByPeriodRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _pepService.ConsultaPep(request.Cpf, request.DataInicioExercicio, request.DataFimExercicio);
            if (entity == null) return NotFound();

            return Ok(entity);
        }
    }
}