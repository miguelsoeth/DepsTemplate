using Autofac;
using DepsTemplate.Core.Interfaces;
using DepsTemplate.Core.Services;

namespace DepsTemplate.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PerfilMetricaService>()
                .As<IPerfilMetricaService>().InstancePerLifetimeScope();

            builder.RegisterType<PerfilService>()
                .As<IPerfilService>().InstancePerLifetimeScope();

            builder.RegisterType<PepService>()
                .As<IPepService>().InstancePerLifetimeScope();

            builder.RegisterType<CepimService>()
                .As<ICepimService>().InstancePerLifetimeScope();
        }
    }
}
