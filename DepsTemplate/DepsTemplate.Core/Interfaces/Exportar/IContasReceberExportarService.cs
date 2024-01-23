using System;
using System.IO;

namespace DepsTemplate.Core.Interfaces
{
    public interface IContasReceberExportarService
    {
        MemoryStream GerarArquivoContasReceber(Guid clienteId, string documento, string pesquisa);
    }
}
