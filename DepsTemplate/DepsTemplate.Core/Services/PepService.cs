using AutoMapper;
using DepsTemplate.Core.DTO;
using DepsTemplate.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Services
{
    public class PepService : IPepService
    {
        private readonly IMapper _mapper;
        private readonly IPortalTransparenciaAPI _portalTransparenciaAPI;

        public PepService(IMapper mapper, IPortalTransparenciaAPI portalTransparenciaAPI)
        {
            _mapper = mapper;
            _portalTransparenciaAPI = portalTransparenciaAPI;
        }

        public async Task<ResponseGenerico<List<PepDto>>> ConsultaPep(string cpf, string periodoInicial, string periodoFinal)
        {
            var peps = await _portalTransparenciaAPI.ConsultaPep(cpf, periodoInicial, periodoFinal);
            return _mapper.Map<ResponseGenerico<List<PepDto>>>(peps);
        }
    }
}
