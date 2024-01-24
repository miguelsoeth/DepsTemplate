
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DepsTemplate.Web.Endpoints.PepEndpoints
{
    public class GetPepByPeriodRequest
    {
        public const string Route = "peps";

        [FromQuery]
        [Required]
        [RegularExpression("^[0-9]{11}$")]
        public string Cpf { get; set; }

        [FromQuery]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$")]
        public string DataInicioExercicio { get; set; }

        [FromQuery]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$")]
        public string DataFimExercicio { get; set; }
    }
}
