using DepsTemplate.Web.Base;
using Microsoft.AspNetCore.Mvc;

namespace DepsTemplate.Web.Endpoints.PerfilEndpoints
{
    public class ListPerfilRequest : PagedRequest
    {
        public const string Route = "perfil";

        [FromQuery]
        public bool Ativo { get; set; }

        [FromQuery]
        public string Filter { get; set; }
    }
}
