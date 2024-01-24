
using DepsTemplate.Core.DTO;
using DepsTemplate.Core.ExternalModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Interfaces
{
    public interface IPortalTransparenciaAPI
    {
        Task<ResponseGenerico<List<ExternalPepModelResponse>>> ConsultaPep(string cpf, string periodoInicial, string periodoFinal);
        Task<ResponseGenerico<List<ExternalCepimModelResponse>>> ConsultaCepim(string cnpj);
    }
}
