using Autofac;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Services;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take
{
    public static class ProgramContainerFactory
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();

            // Program
            builder.RegisterType<ProgramRunner>();
            builder.RegisterType<ProgramConfiguration>().AsImplementedInterfaces();
            builder.RegisterType<OctopusRepositoryFactory>();
            builder.RegisterType<SnapshotEnvironmentProvider>().AsImplementedInterfaces();
            builder.RegisterType<Services.SnapshotWriter>().AsImplementedInterfaces();
            builder.Register(context => context.Resolve<OctopusRepositoryFactory>().Create()).InstancePerLifetimeScope();

            // Domain
            builder.RegisterType<SnapshotBuilder>().AsImplementedInterfaces();
            builder.RegisterType<SnapshotComponentBuilder>().AsImplementedInterfaces();
            builder.RegisterType<SnapshotComponentVersionBuilder>().AsImplementedInterfaces();
            builder.RegisterType<SnapshotStepBuilder>().AsImplementedInterfaces();

            return builder.Build();
        }
    }
}