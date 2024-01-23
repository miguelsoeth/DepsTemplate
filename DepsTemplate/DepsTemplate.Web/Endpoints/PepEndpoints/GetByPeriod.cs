using Ardalis.ApiEndpoints;
using DepsTemplate.Core.Interfaces;
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
    [ApiController]
    [Route("api/v{version:apiVersion}/pep")]
    [AllowAnonymous]
    [EnableCors("AllowSpecificOrigin")]
    public class GetByPeriod : ControllerBase
    {
        public readonly IPepService _pepService;

        public GetByPeriod(IPepService pepService)
        {
            _pepService = pepService;
        }
        [HttpGet]
        [SwaggerOperation(
          Summary = "Obtém um pep por periodo",
          OperationId = "Peps.GetByPeriod",
          Tags = new[] { "PepEndpoints" })
        ]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> PepConsultaPorCPF([FromQuery][Required][RegularExpression("^[0-9]*$")] string cpf, [FromQuery][RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$")] string periodoInicial, [FromQuery][RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$")] string periodoFinal)
        {
            if (!VerifyCpf.IsValid(cpf))
            {
                return BadRequest("CPF inválido");
            }

            var response = await _pepService.ConsultaPep(cpf, periodoInicial, periodoFinal);
            if (response.CodigoHttp == HttpStatusCode.OK)
            {
                return Ok(response.DadosRetorno);
            }
            else
            {
                return StatusCode((int)response.CodigoHttp, response.ErroRetorno);
            }
        }
    }
}
/*
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [AllowAnonymous]
    public class GetByPeriod : EndpointBaseAsync.WithRequest<GetPepByPeriodRequest>.WithActionResult<GetPepByPeriodResponse>
    {
        [HttpGet(GetPerfilByIdRequest.Route)]
        [SwaggerOperation(
          Summary = "Obtém um perfil por id",
          OperationId = "Perfil.GetById",
          Tags = new[] { "PerfilEndpoints" })
        ]
        public override Task<ActionResult<GetPepByPeriodResponse>> HandleAsync(GetPepByPeriodRequest request, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
*/