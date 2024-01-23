//using DepsTemplate.Core.MetricaAggregate;
//using DepsTemplate.Core.Entities.PerfilAggregate;
//using DepsTemplate.Core.Entities.PerfilAggregate.Specifications;
//using DepsTemplate.Core.Services;
//using DepsTemplate.SharedKernel.Interfaces;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Xunit;

//namespace DepsTemplate.UnitTests.Core.Services.ParametrizacaoMetricaServiceTest
//{
//    public class AdicionarParametrizacoesMetricaPorMetricaAsync
//    {
//        private readonly Mock<IRepository<ParametrizacaoMetrica>> _mockRepository;
//        private readonly Mock<IRepository<Perfil>> _mockPerfilRepository;

//        public AdicionarParametrizacoesMetricaPorMetricaAsync()
//        {
//            _mockRepository = new Mock<IRepository<ParametrizacaoMetrica>>();
//            _mockPerfilRepository = new Mock<IRepository<Perfil>>();
//        }

//        [Fact]
//        public async Task CreatesNewParametrizacaoGivenMetrica()
//        {
//            var parametrizacaoService = new ParametrizacaoMetricaService(_mockPerfilRepository.Object, null, _mockRepository.Object);

//            var perfil = Perfil.Factory.NovoPerfil("Teste perfil", 1);
//            perfil.Id = 1;
//            var listPerfil = new List<Perfil> { perfil };

//            _mockPerfilRepository.Setup(r => r.ListAsync(It.IsAny<PerfisPorAtivoSpec>(), default)).ReturnsAsync(listPerfil);

//            var metrica = Metrica.Factory.NovaMetrica("Teste");
//            metrica.Id = 2;

//            await parametrizacaoService.AdicionarParametrizacoesMetricaPorMetricaAsync(metrica);

//            _mockRepository.Verify(x => x.AddAsync(It.Is<ParametrizacaoMetrica>(x => x.MetricaId == 2 && x.PerfilId == 1), default), Times.Once);
//        }

//        [Fact]
//        public async Task ThrowsGivenNullMetrica()
//        {
//            var parametrizacaoService = new ParametrizacaoMetricaService(null, null, null);

//            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await parametrizacaoService.AdicionarParametrizacoesMetricaPorMetricaAsync(null));
//            Assert.Equal("metrica", ex.ParamName);
//        }
//    }
//}
