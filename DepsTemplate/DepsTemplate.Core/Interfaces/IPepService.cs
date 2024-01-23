
using DepsTemplate.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Interfaces
{
    public interface IPepService
    {
        Task<ResponseGenerico<List<PepDto>>> ConsultaPep(string cpf, string periodoInicial, string periodoFinal);
    }
}
