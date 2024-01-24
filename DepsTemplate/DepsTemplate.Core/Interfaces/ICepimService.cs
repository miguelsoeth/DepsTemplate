using DepsTemplate.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Interfaces
{
    public interface ICepimService
    {
        Task<ResponseGenerico<List<CepimDto>>> ConsultaCepim(string cnpj);
    }
}
