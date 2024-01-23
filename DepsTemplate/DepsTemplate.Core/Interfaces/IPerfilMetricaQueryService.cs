using DepsTemplate.Core.DTO;
using System.Collections.Generic;

namespace DepsTemplate.Core.Interfaces
{
    public interface IPerfilMetricaQueryService
    {
        List<ParametrizacaoMetricaDto> ListParametrizacaoMetricaPorPerfilOuMetrica(int? perfilId, int? metricaId);
    }
}
