using PrismaCloudVSExtension.Entities.Checkov;
using PrismaCloudVSExtension.Entities.Config;
using PrismaCloudVSExtension.Services.Contracts.Common;
using PrismaCloudVSExtension.Services.Contracts.Installation;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PrismaCloudVSExtension.Services.Checkov
{
    public class CheckovInstallationService
    {
        private IInstallationHandler _handler;
        private readonly IConfigService _configService;

        public CheckovInstallationService (
                IConfigService configService,
                IDockerInstallationHandler dockerInstallationHandler,
                IPip3InstallationHandler pip3InstallationHandler,
                IPipenvInstallationHandler pipenvInstallationHandler
            )
        {
            _configService = configService;
            _handler = dockerInstallationHandler;
            _handler
                .SetNext(pip3InstallationHandler)
                .SetNext(pipenvInstallationHandler);
        }

        public async Task Install(InstallationContext context)
        {
            Debug.WriteLine("[DEBUG] Run installation handler");

            var result = await _handler.Handle(context);

            if (result?.Status == true)
            {
                // Update installation data in config 
                _configService.Config.Installation = result;

                // Debug
                Debug.WriteLine(_configService.Config.Checkov.Version, "[DEBUG] Checkov version");
                Debug.WriteLine(_configService.Config.Requirements.MinPythonVersion, "[DEBUG] Checkov Requirements");

                Debug.WriteLine(_configService.Config.Installation.Entrypoint, "[DEBUG] Installation Entrypoint");
                Debug.WriteLine(_configService.Config.Installation.Type, "[DEBUG] Installation Type");
            }
        }
    }
}
