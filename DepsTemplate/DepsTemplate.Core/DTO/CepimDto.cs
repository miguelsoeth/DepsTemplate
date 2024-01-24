namespace DepsTemplate.Core.DTO

{
    public class CepimDto
    {
        public int Id { get; set; }
        public string DataReferencia { get; set; }
        public string Motivo { get; set; }
        public OrgaoSuperiorDto OrgaoSuperior { get; set; }
        public PessoaJuridicaDto PessoaJuridica { get; set; }
        public ConvenioDto Convenio { get; set; }
    }

    public class ConvenioDto
    {
        public string Codigo { get; set; }
        public string Objeto { get; set; }
        public string Numero { get; set; }
    }

    public class OrgaoSuperiorDto
    {
        public string Nome { get; set; }
        public string CodigoSIAFI { get; set; }
        public string Cnpj { get; set; }
        public string Sigla { get; set; }
        public string DescricaoPoder { get; set; }
        public OrgaoMaximoDto OrgaoMaximo { get; set; }
    }

    public class OrgaoMaximoDto
    {
        public string Codigo { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
    }

    public class PessoaJuridicaDto
    {
        public int Id { get; set; }
        public string CpfFormatado { get; set; }
        public string CnpjFormatado { get; set; }
        public string NumeroInscricaoSocial { get; set; }
        public string Nome { get; set; }
        public string RazaoSocialReceita { get; set; }
        public string NomeFantasiaReceita { get; set; }
        public string Tipo { get; set; }
    }
}
