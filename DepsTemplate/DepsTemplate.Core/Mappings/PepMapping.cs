using AutoMapper;
using DepsTemplate.Core.DTO;
using DepsTemplate.Core.ExternalModels;

namespace DepsTemplate.Core.Mappings
{
    public class PepMapping: Profile
    {
        public PepMapping()
        {
            CreateMap(typeof(ResponseGenerico<>), typeof(ResponseGenerico<>));
            CreateMap<PepDto, ExternalPepModelResponse>();
            CreateMap<ExternalPepModelResponse, PepDto>();
        }
    }
}
