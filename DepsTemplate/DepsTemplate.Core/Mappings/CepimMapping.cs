using AutoMapper;
using DepsTemplate.Core.DTO;
using DepsTemplate.Core.ExternalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Mappings
{
    public class CepimMapping : Profile
    {
        public CepimMapping()
        {
            CreateMap(typeof(ResponseGenerico<>), typeof(ResponseGenerico<>));

            CreateMap<CepimDto, ExternalCepimModelResponse>();
            CreateMap<ExternalCepimModelResponse, CepimDto>();

            CreateMap<ConvenioModelResponse, ConvenioDto>();
            CreateMap<ConvenioDto, ConvenioModelResponse>();

            CreateMap<OrgaoMaximoModelResponse, OrgaoMaximoDto>();
            CreateMap<OrgaoMaximoDto, OrgaoMaximoModelResponse>();


            CreateMap<OrgaoSuperiorModelResponse, OrgaoSuperiorDto>();
            CreateMap<OrgaoSuperiorDto, OrgaoSuperiorModelResponse>();

            CreateMap<PessoaJuridicaModelResponse, PessoaJuridicaDto>();
            CreateMap<PessoaJuridicaDto, PessoaJuridicaModelResponse>();

        }
    }
}
