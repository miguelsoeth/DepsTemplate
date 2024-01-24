using AutoMapper;
using DepsTemplate.Core.DTO;
using DepsTemplate.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Services
{
    public class CepimService : ICepimService
    {
        private readonly IMapper _mapper;
        private readonly IPortalTransparenciaAPI _portalTransparenciaAPI;

        public CepimService(IMapper mapper, IPortalTransparenciaAPI portalTransparenciaAPI)
        {
            _mapper = mapper;
            _portalTransparenciaAPI = portalTransparenciaAPI;
        }

        public async Task<ResponseGenerico<List<CepimDto>>> ConsultaCepim(string cnpj)
        {
            var cepim = await _portalTransparenciaAPI.ConsultaCepim(cnpj);
            return _mapper.Map<ResponseGenerico<List<CepimDto>>>(cepim);
        }
    }
}
