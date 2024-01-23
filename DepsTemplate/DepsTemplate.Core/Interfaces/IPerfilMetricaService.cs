using DepsTemplate.Core.DTO;
using DepsTemplate.Core.Entities.PerfilAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Interfaces
{
    public interface IPerfilMetricaService
    {
        Task AdicionarPerfilMetricaPorPerfilAsync(Perfil perfil);
        Task AtualizarPerfisMetricasAsync(List<CadastroPerfilMetricaDto> perfisMetricas);
    }
}
