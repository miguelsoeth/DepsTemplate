using DepsTemplate.Core.Enums;
using System;
using System.IO;

namespace DepsTemplate.Core.Interfaces
{
    public interface IContasReceberExportarDetalhadoService
    {
        MemoryStream GerarArquivoContasReceberDetalhado(Guid clienteId, string documento, TipoContaReceber tipo);
    }
}
