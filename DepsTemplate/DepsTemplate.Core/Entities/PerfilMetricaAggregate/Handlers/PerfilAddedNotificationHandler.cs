using Ardalis.GuardClauses;
using DepsTemplate.Core.Entities.PerfilAggregate.Events;
using DepsTemplate.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Entities.PerfilMetricaAggregate.Handlers
{
    public class PerfilAddedNotificationHandler : INotificationHandler<PerfilAddedEvent>
    {
        private readonly IPerfilMetricaService _perfilMetricaService;

        public PerfilAddedNotificationHandler(IPerfilMetricaService perfilMetricaService)
        {
            _perfilMetricaService = perfilMetricaService;
        }

        public async Task Handle(PerfilAddedEvent notification, CancellationToken cancellationToken)
        {
            Guard.Against.Null(notification, nameof(notification));
            await _perfilMetricaService.AdicionarPerfilMetricaPorPerfilAsync(notification.Perfil);
        }
    }
}
