using Autofac;
using PrismaCloudVSExtension.Services.Checkov;
using PrismaCloudVSExtension.Services.Common;
using PrismaCloudVSExtension.Services.Contracts.Common;
using PrismaCloudVSExtension.Services.Contracts.Installation;

namespace PrismaCloudVSExtension.Win
{
    public static class Bootstrap
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            // Services
            builder.RegisterType<ConfigService>().As<IConfigService>().SingleInstance();
            builder.RegisterType<CheckovInstallationService>().AsSelf().SingleInstance();

            // Handlers
            builder.RegisterType<DockerInstallationHandler>().As<IDockerInstallationHandler>();
            builder.RegisterType<Pip3InstallationHandler>().As<IPip3InstallationHandler>();
            builder.RegisterType<PipenvInstallationHandler>().As<IPipenvInstallationHandler>();


            return builder.Build();
        }
    }
}
