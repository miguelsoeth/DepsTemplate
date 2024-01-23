using Ardalis.Specification;
using DepsTemplate.Core.Entities.PerfilAggregate;
using System.Linq;

namespace DepsTemplate.Core.Entities.PerfilAggregate.Specifications
{
    public class PerfisComOrdemMaiorOuIgualSpec : Specification<Perfil>
    {
        public PerfisComOrdemMaiorOuIgualSpec(int ordem)
        {
            Query.AsNoTracking().Where(perfil => perfil.Ordem >= ordem)
                .OrderBy(perfil => perfil.Ordem);
        }
    }
}
