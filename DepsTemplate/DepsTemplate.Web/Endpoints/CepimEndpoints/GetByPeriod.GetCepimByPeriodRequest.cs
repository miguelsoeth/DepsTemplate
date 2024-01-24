
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DepsTemplate.Web.Endpoints.CepimEndpoints
{
    public class GetCepimByPeriodRequest
    {
        public const string Route = "cepim";

        [FromQuery]
        [Required]
        [RegularExpression("^[0-9]{14}$")]
        public string Cnpj { get; set; }

        [FromQuery]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$")]
        public string DataInicioExercicio { get; set; }

        [FromQuery]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$")]
        public string DataFimExercicio { get; set; }
    }
}
