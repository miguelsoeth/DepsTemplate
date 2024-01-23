using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepsTemplate.Core.DTO
{
    public class PepDto
    {
        public string? Cpf { get; set; }
        public string? Nome { get; set; }
        public string? DescricaoFuncao { get; set; }
        public string? NomeOrgao { get; set; }
        public string? DataInicioExercicio { get; set; }
        public string? DataFimExercicio { get; set; }
        public string? CodigoOrgao { get; set; }
        public string? SiglaFuncao { get; set; }
        public string? NivelFuncao { get; set; }
        public string? DataFimCarencia { get; set; }
    }
}
