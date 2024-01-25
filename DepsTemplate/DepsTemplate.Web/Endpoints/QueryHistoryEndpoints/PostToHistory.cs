using Ardalis.ApiEndpoints;
using DepsTemplate.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System.Threading;
using DepsTemplate.Core.ExternalModels;
using Npgsql;
using System;
using DepsTemplate.Core.DTO;
using System.Collections.Generic;
using DepsTemplate.SharedKernel.Util;

namespace DepsTemplate.Web.Endpoints.QueryHistoryEndpoints
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [AllowAnonymous]
    public class PostToHistory : EndpointBaseAsync.WithRequest<PostToHistoryRequest>.WithActionResult<PostToHistoryResponse>
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public PostToHistory(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost(PostToHistoryRequest.Route)]
        [SwaggerOperation(
            Summary = "Adicionar pesquisa ao histórico",
            Description = "Adicionar pesquisa ao histórico",
            OperationId = "QueryHistory.Post",
            Tags = new[] { "QueryEndpoints" })
        ]
        [ProducesResponseType(typeof(QueryHistoryDto), 200)]
        public override async Task<ActionResult<PostToHistoryResponse>> HandleAsync(
            PostToHistoryRequest request,
            CancellationToken cancellationToken = default)
        {
            var connection = new NpgsqlConnection(AmbienteUtil.GetValue("POSTGRES_CONNECTION"));
            await connection.OpenAsync();
            var command = new NpgsqlCommand
            {
                Connection = connection,
                CommandText = "INSERT INTO public.query_history_model (username, type, document, referredDate, interval, interval_label) " +
                                  "VALUES (@username, @type, @document, @referredDate, @interval, @interval_label) RETURNING *"
            };
            command.Parameters.AddWithValue("username", request.username);
            command.Parameters.AddWithValue("type", request.type);
            command.Parameters.AddWithValue("document", request.document);
            command.Parameters.AddWithValue("referredDate", request.referredDate);
            command.Parameters.AddWithValue("interval", request.interval);
            command.Parameters.AddWithValue("interval_label", request.interval_label);

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    var result = new PostToHistoryResponse
                    {
                        id = Convert.ToInt32(reader["id"]), // Assuming "id" is the name of the column in the database
                        username = reader["username"].ToString(),
                        querydate = DateTime.UtcNow, // Assuming you want to set the querydate to the current UTC time
                        type = reader["type"].ToString(),
                        document = reader["document"].ToString(),
                        referreddate = Convert.ToDateTime(reader["referredDate"]),
                        interval = reader["interval"].ToString(),
                        interval_label = reader["interval_label"].ToString()
                    };
                    reader.Close();
                    return result;
                }
            }
            return NotFound();
        }
    }
}
