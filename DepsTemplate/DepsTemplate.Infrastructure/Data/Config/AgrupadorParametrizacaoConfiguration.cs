using DepsTemplate.Core.Entities.PerfilMetricaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepsTemplate.Infrastructure.Data.Config
{
    public class AgrupadorParametrizacaoConfiguration : IEntityTypeConfiguration<AgrupadorParametrizacao>
    {
        public void Configure(EntityTypeBuilder<AgrupadorParametrizacao> builder)
        {
            builder.Property(p => p.Nome)
               .IsRequired();
        }
    }
}
