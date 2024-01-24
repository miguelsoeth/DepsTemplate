using Ardalis.ApiEndpoints;
using DepsTemplate.Core.DTO;
using DepsTemplate.Core.Enums;
using DepsTemplate.Core.Interfaces;
using DepsTemplate.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepsTemplate.Web.Endpoints.CepimEndpoints
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [AllowAnonymous]
    public class GetByPeriod : EndpointBaseAsync.WithRequest<GetCepimByPeriodRequest>.WithActionResult<GetCepimByPeriodResponse>
    {
        private readonly ICepimService _cepimService;

        public GetByPeriod(ICepimService cepimService)
        {
            _cepimService = cepimService;
        }

        [HttpGet(GetCepimByPeriodRequest.Route)]
        [SwaggerOperation(
          Summary = "Obtém cepim por periodo",
          OperationId = "Cepim.GetByPeriod",
          Tags = new[] { "CepimEndpoints" })
        ]
        [ProducesResponseType(typeof(List<CepimDto>), 200)]
        public override async Task<ActionResult<GetCepimByPeriodResponse>> HandleAsync(
            [FromQuery] GetCepimByPeriodRequest request, 
            CancellationToken cancellationToken = default)
        {
            if (!VerifyCnpj.IsValid(request.Cnpj)) return BadRequest(new { error = "CNPJ Inválido" });

            var entity = await _cepimService.ConsultaCepim(request.Cnpj);

            if (entity == null) return NotFound();

            try
            {                
                DateOnly.TryParseExact(request.DataInicioExercicio, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly dataInicial);
                DateOnly.TryParseExact(request.DataFimExercicio, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly dataFinal);

                if (dataFinal == dataInicial) return BadRequest(new { error = "Datas inválida" });
                        
                var filteredResponse = entity.DadosRetorno.Where(item =>
                        DateOnly.TryParseExact(item.DataReferencia, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly refDate)
                        && refDate <= dataFinal
                        && refDate >= dataInicial)
                        .ToList();

                return Ok(filteredResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }
    }
}