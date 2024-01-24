using System.Text.Json.Serialization;

namespace DepsTemplate.Core.ExternalModels
{
    public class ExternalCepimModelResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("dataReferencia")]
        public string DataReferencia { get; set; }

        [JsonPropertyName("motivo")]
        public string Motivo { get; set; }

        [JsonPropertyName("orgaoSuperior")]
        public OrgaoSuperiorModelResponse OrgaoSuperior { get; set; }

        [JsonPropertyName("pessoaJuridica")]
        public PessoaJuridicaModelResponse PessoaJuridica { get; set; }

        [JsonPropertyName("convenio")]
        public ConvenioModelResponse Convenio { get; set; }
    }

    public class ConvenioModelResponse
    {
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }

        [JsonPropertyName("objeto")]
        public string Objeto { get; set; }

        [JsonPropertyName("numero")]
        public string Numero { get; set; }
    }

    public class OrgaoMaximoModelResponse
    {
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }

        [JsonPropertyName("sigla")]
        public string Sigla { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }

    public class OrgaoSuperiorModelResponse
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("codigoSIAFI")]
        public string CodigoSIAFI { get; set; }

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; }

        [JsonPropertyName("sigla")]
        public string Sigla { get; set; }

        [JsonPropertyName("descricaoPoder")]
        public string DescricaoPoder { get; set; }

        [JsonPropertyName("orgaoMaximo")]
        public OrgaoMaximoModelResponse OrgaoMaximo { get; set; }
    }

    public class PessoaJuridicaModelResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cpfFormatado")]
        public string CpfFormatado { get; set; }

        [JsonPropertyName("cnpjFormatado")]
        public string CnpjFormatado { get; set; }

        [JsonPropertyName("numeroInscricaoSocial")]
        public string NumeroInscricaoSocial { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("razaoSocialReceita")]
        public string RazaoSocialReceita { get; set; }

        [JsonPropertyName("nomeFantasiaReceita")]
        public string NomeFantasiaReceita { get; set; }

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }
    }
}
