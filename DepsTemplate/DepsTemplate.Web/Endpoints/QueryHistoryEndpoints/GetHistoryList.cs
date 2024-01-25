using Ardalis.ApiEndpoints;
using DepsTemplate.Infrastructure.Data;
using DepsTemplate.Web.Endpoints.PepEndpoints;
using DepsTemplate.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DepsTemplate.Web.Endpoints.QueryHistoryEndpoints
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [AllowAnonymous]
    public class GetHistoryList : EndpointBaseAsync.WithoutRequest.WithActionResult<GetHistoryListResponse>
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public GetHistoryList(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet("historico-pesquisa/")]
        [SwaggerOperation(
          Summary = "Obtém histórico de pesquisas",
          OperationId = "QueryHistory.GetHistoryList",
          Tags = new[] { "QueryEndpoints" })
        ]
        public override async Task<ActionResult<GetHistoryListResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var history = await _context.queries.OrderByDescending(q => q.querydate).Take(500).ToListAsync();
            if (history == null) return NotFound();

            return Ok(history);
        }
    }
}
