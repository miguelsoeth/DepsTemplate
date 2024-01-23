using DepsTemplate.Core.DTO;
using DepsTemplate.Core.Entities.PerfilAggregate;
using DepsTemplate.Core.Services;
using DepsTemplate.SharedKernel.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DepsTemplate.UnitTests.Core.Services.PerfilServiceTest
{
    public class OrdenarPerfis
    {
        private readonly Mock<IRepository<Perfil>> _mockPerfilRepository;

        public OrdenarPerfis()
        {
            _mockPerfilRepository = new Mock<IRepository<Perfil>>();
        }

        [Fact]
        public async Task OrdenarPerfisGivenDifferentOrder()
        {
            var perfilService = new PerfilService(_mockPerfilRepository.Object);

            _mockPerfilRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default))
                .ReturnsAsync(Perfil.Factory.NovoPerfil("Conservador", 1));

            var data = new List<OrdenacaoPerfilDto>
            {
                new OrdenacaoPerfilDto
                {
                    Ordem = 15
                },
                new OrdenacaoPerfilDto
                {
                    Ordem = 16
                },
                new OrdenacaoPerfilDto
                {
                    Ordem = 17
                },
            };

            await perfilService.OrdenarPerfisAsync(data);

            _mockPerfilRepository.Verify(x => x.UpdateAsync(It.IsAny<Perfil>(), default), Times.Exactly(3));
            _mockPerfilRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), default), Times.Exactly(3));
        }
    }
}
