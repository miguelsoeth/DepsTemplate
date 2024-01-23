using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepsTemplate.Web.Endpoints.PepEndpoints
{
    public class GetPepByPeriodRequest
    {
        public const string Route = "peps/{PerfilId:int}";
        public static string BuildRoute(int perfilId) => Route.Replace("{PerfilId:int}", perfilId.ToString());
        public string? Cpf { get; set; }
        public string? DataInicioExercicio { get; set; }
        public string? DataFimExercicio { get; set; }
    }
}
