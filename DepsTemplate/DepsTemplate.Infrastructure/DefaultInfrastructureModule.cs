﻿using Autofac;
using DepsTemplate.Core.Entities.PerfilAggregate;
using DepsTemplate.Core.Interfaces;
using DepsTemplate.Infrastructure.Data;
using DepsTemplate.Infrastructure.Data.Queries;
using DepsTemplate.Infrastructure.Exportar;
using DepsTemplate.Infrastructure.Portal;
using DepsTemplate.Infrastructure.Storage;
using DepsTemplate.SharedKernel.Interfaces;
using MediatR;
using MediatR.Pipeline;
using System.Collections.Generic;
using System.Reflection;
using Module = Autofac.Module;

namespace DepsTemplate.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private readonly bool _isDevelopment = false;
        private readonly List<Assembly> _assemblies = new();

        public DefaultInfrastructureModule(bool isDevelopment, Assembly callingAssembly = null)
        {
            _isDevelopment = isDevelopment;
            var coreAssembly = Assembly.GetAssembly(typeof(Perfil));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(StartupSetup));
            if (coreAssembly != null)
            {
                _assemblies.Add(coreAssembly);
            }
            if (infrastructureAssembly != null)
            {
                _assemblies.Add(infrastructureAssembly);
            }
            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .As(typeof(IReadRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(MongoRepository<>))
                .As(typeof(INoSqlRepository<>)).InstancePerDependency();

            builder.RegisterType<PerfilMetricaQueryService>()
                .As<IPerfilMetricaQueryService>().InstancePerLifetimeScope();

            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ComunicacaoPortalService>()
                .As<IComunicacaoPortalService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<MongoContext>()
                .SingleInstance();

            builder
                .RegisterType<ContasReceberStorageService>()
                .As<IContasReceberStorageService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ContasReceberExportarService>()
                .As<IContasReceberExportarService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ContasReceberExportarDetalhadoService>()
                .As<IContasReceberExportarDetalhadoService>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                .RegisterAssemblyTypes(_assemblies.ToArray())
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
            }
        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add development only services
        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add production only services
        }

    }
}
