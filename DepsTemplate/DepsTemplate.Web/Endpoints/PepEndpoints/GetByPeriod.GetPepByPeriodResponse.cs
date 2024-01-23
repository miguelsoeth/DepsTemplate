using System.Text.Json.Serialization;

namespace DepsTemplate.Web.Endpoints.PepEndpoints
{
    public class GetPepByPeriodResponse
    {
        [JsonPropertyName("cpf")]
        public string? Cpf { get; set; }

        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("sigla_funcao")]
        public string? SiglaFuncao { get; set; }

        [JsonPropertyName("descricao_funcao")]
        public string? DescricaoFuncao { get; set; }

        [JsonPropertyName("nivel_funcao")]
        public string? NivelFuncao { get; set; }

        [JsonPropertyName("cod_orgao")]
        public string? CodigoOrgao { get; set; }

        [JsonPropertyName("nome_orgao")]
        public string? NomeOrgao { get; set; }

        [JsonPropertyName("dt_inicio_exercicio")]
        public string? DataInicioExercicio { get; set; }

        [JsonPropertyName("dt_fim_exercicio")]
        public string? DataFimExercicio { get; set; }

        [JsonPropertyName("dt_fim_carencia")]
        public string? DataFimCarencia { get; set; }
    }
}
