using DepsTemplate.Core.Entities.PerfilAggregate;
using DepsTemplate.Core.Entities.PerfilMetricaAggregate;
using System.Collections.Generic;

namespace DepsTemplate.Core.DTO
{
    public class ParametrizacaoMetricaDto
    {
        public int Id { get; set; }
        public Perfil Perfil { get; set; }
        public decimal PontuacaoMaxima { get; set; }
        public decimal PontuacaoMinima { get; set; }
        public int? Validade { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<ParametrizacaoMetrica> Parametrizacoes { get; set; }
    }
}
