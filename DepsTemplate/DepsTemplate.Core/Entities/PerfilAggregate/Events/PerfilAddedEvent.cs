using DepsTemplate.Core.Entities.PerfilAggregate;
using DepsTemplate.SharedKernel;

namespace DepsTemplate.Core.Entities.PerfilAggregate.Events
{
    public class PerfilAddedEvent : BaseDomainEvent
    {
        public PerfilAddedEvent(Perfil perfil)
        {
            Perfil = perfil;
        }

        public Perfil Perfil { get; set; }

    }
}
