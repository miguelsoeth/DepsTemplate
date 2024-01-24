using AutoMapper;
using DepsTemplate.Core.DTO;
using DepsTemplate.Infrastructure.ExternalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
