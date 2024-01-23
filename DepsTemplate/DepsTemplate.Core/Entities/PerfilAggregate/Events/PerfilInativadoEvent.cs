using DepsTemplate.Core.Entities.PerfilAggregate;
using DepsTemplate.SharedKernel;

namespace DepsTemplate.Core.Entities.PerfilAggregate.Events
{
    public class PerfilInativadoEvent : BaseDomainEvent
    {
        public PerfilInativadoEvent(Perfil perfil)
        {
            Perfil = perfil;
        }

        public Perfil Perfil { get; set; }
    }
}
