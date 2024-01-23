using DepsTemplate.Core.DTO;
using DepsTemplate.Core.Entities.PerfilAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Interfaces
{
    public interface IPerfilService
    {
        Task<Perfil> CreatPerfilAsync(string nome, int ordem);
        Task<Perfil> UpdatePerfilAsync(int id, string nome, bool ativo);
        Task OrdenarPerfisAsync(List<OrdenacaoPerfilDto> ordenacao);
    }
}
