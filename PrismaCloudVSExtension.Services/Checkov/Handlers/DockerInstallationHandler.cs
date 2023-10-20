using PrismaCloudVSExtension.Entities.Checkov;
using PrismaCloudVSExtension.Entities.Hubs;
using PrismaCloudVSExtension.Services.Contracts.Installation;
using PrismaCloudVSExtension.Utils.Commands;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using PrismaCloudVSExtension.Services.Contracts.Common;

namespace PrismaCloudVSExtension.Services.Checkov
{
    public class  DockerInstallationHandler : InstallationAbstractHandler, IDockerInstallationHandler
    {

        public DockerInstallationHandler(IConfigService configService) : base(configService)
        {
            
        }

        public async override Task<InstallationResult> Handle(InstallationContext context)
        {
            var result = await _tryInstall(context);

            if (result.Status == true) return result;

            return await base.Handle(context);
        }

        private async Task<InstallationResult> _tryInstall(InstallationContext context)
        {
            Debug.WriteLine("[DEBUG] Docker installation process start");

            try
            {
                string result = await AsyncExec.run("docker", $"pull bridgecrew/checkov:" + _configService.Config.Checkov.Version);
                Debug.WriteLine("[DEBUG] Docker installation success", result);

                return new InstallationResult { Status = true, Type = InstallationHub.Docker, Entrypoint = "docker" };
            }
            catch (Exception e)
            {
                Debug.WriteLine("[ERROR] Docker installation fail", e.Message);
            }

            return new InstallationResult { Status = false, Type = InstallationHub.Docker };

        }
    }
}
