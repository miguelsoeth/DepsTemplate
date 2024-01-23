using Ardalis.GuardClauses;
using DepsTemplate.Core.DTO;
using DepsTemplate.Core.Entities.PerfilAggregate;
using DepsTemplate.Core.Entities.PerfilAggregate.Events;
using DepsTemplate.Core.Entities.PerfilAggregate.Specifications;
using DepsTemplate.Core.Interfaces;
using DepsTemplate.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IRepository<Perfil> _repository;

        public PerfilService(IRepository<Perfil> repository)
        {
            _repository = repository;
        }

        public async Task<Perfil> CreatPerfilAsync(string nome, int ordem)
        {
            Guard.Against.NullOrWhiteSpace(nome, nameof(nome));
            Guard.Against.NegativeOrZero(ordem, nameof(ordem));

            var perfil = Perfil.Factory.NovoPerfil(nome, ordem);
            perfil.Events.Add(new PerfilAddedEvent(perfil));

            await OrdenarPerfis(perfil.Ordem);

            await _repository.AddAsync(perfil);

            return perfil;
        }

        private async Task OrdenarPerfis(int ordem)
        {
            var ordemMaiorOuIgualSpec = new PerfisComOrdemMaiorOuIgualSpec(ordem);
            var perfisComOrdenacaoMaior = await _repository.ListAsync(ordemMaiorOuIgualSpec);

            foreach (var perfilOrdenacao in perfisComOrdenacaoMaior)
            {
                perfilOrdenacao.AlterarOrdem(perfilOrdenacao.Ordem + 1);
                await _repository.UpdateAsync(perfilOrdenacao);
            }
        }

        public async Task OrdenarPerfisAsync(List<OrdenacaoPerfilDto> ordenacao)
        {
            foreach (var item in ordenacao)
            {
                var perfil = await _repository.GetByIdAsync(item.Id);
                if (perfil.HasOrdemChanged(item.Ordem))
                {
                    perfil.AlterarOrdem(item.Ordem);
                    await _repository.UpdateAsync(perfil);
                }
            }
        }

        public async Task<Perfil> UpdatePerfilAsync(int id, string nome, bool ativo)
        {
            Guard.Against.NullOrWhiteSpace(nome, nameof(nome));

            var perfil = await _repository.GetByIdAsync(id);
            if (!perfil.HasNameChanged(nome) && !perfil.HasAtivoChanged(ativo))
            {
                return null;
            }

            perfil.AlterarNome(nome);
            if (perfil.HasAtivoChanged(ativo))
            {
                perfil.Inativar();
                perfil.Events.Add(new PerfilInativadoEvent(perfil));
            }

            await _repository.UpdateAsync(perfil);

            return perfil;
        }
    }
}
