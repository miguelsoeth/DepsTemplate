using DepsTemplate.SharedKernel;
using System;
using System.Collections.Generic;

namespace DepsTemplate.Core.Entities.PerfilMetricaAggregate
{
    public class AgrupadorParametrizacao : BaseEntity<Guid>
    {
        public string Nome { get; set; }

        public virtual IEnumerable<ParametrizacaoMetrica> ParametrizacoesMetrica { get; set; }
    }
}
