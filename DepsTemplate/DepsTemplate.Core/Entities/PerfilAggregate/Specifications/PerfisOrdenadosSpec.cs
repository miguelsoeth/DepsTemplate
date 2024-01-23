using Ardalis.Specification;
using DepsTemplate.Core.Entities.PerfilAggregate;
using System.Linq;

namespace DepsTemplate.Core.Entities.PerfilAggregate.Specifications
{
    public class PerfisOrdenadosSpec : Specification<Perfil>
    {
        public PerfisOrdenadosSpec()
        {
            Query.AsNoTracking().Where(perfil => perfil.Ativo)
                 .OrderBy(perfil => perfil.Ordem);
        }
    }
}
