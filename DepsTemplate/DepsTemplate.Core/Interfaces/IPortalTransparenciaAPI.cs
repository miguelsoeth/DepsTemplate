
using DepsTemplate.Core.DTO;
using DepsTemplate.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepsTemplate.Core.Interfaces
{
    public interface IPortalTransparenciaAPI
    {
        Task<ResponseGenerico<List<PepModel>>> ConsultaPep(string cpf, string periodoInicial, string periodoFinal);
    }
}
