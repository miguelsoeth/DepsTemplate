using Ardalis.ApiEndpoints;
using DepsTemplate.Core.Interfaces;
using DepsTemplate.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
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
        public override async Task<ActionResult<GetPepByPeriodResponse>> HandleAsync(
            [FromQuery] GetPepByPeriodRequest request, 
            CancellationToken cancellationToken = default)
        {
            if (!VerifyCpf.IsValid(request.Cpf))
            {
                return BadRequest();
            }
            var entity = await _pepService.PepByPeriod(request.Cpf, request.DataInicioExercicio, request.DataFimExercicio);
            if (entity == null) return NotFound();

            return Ok(entity.DadosRetorno);
        }
    }
}