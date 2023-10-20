using PrismaCloudVSExtension.Entities.Checkov;
using PrismaCloudVSExtension.Entities.Hubs;
using PrismaCloudVSExtension.Services.Contracts.Common;
using PrismaCloudVSExtension.Services.Contracts.Installation;
using PrismaCloudVSExtension.Utils.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PrismaCloudVSExtension.Services.Checkov
{
    public class PipenvInstallationHandler : InstallationAbstractHandler, IPipenvInstallationHandler
    {
        public PipenvInstallationHandler(IConfigService configService) : base(configService)
        {

        }

        public async override Task<InstallationResult> Handle(InstallationContext context)
        {
            var result = await _tryInstall();

            if (result.Status == true) return result;

            return await base.Handle(context);
        }

        private async Task<InstallationResult> _tryInstall()
        {
            Debug.WriteLine("[DEBUG] Pipenv installation process start");

            try
            {
                bool isPythonVersionSuitable = await _isPythonVersionSuitable("pipenv", "run python --version");
                if (isPythonVersionSuitable == true)
                {
                    string result = await AsyncExec.run("pipenv", " --python 3 install checkov~=2.0.0");
                    Debug.WriteLine("[DEBUG] Pipenv installation success", result);

                    return new InstallationResult { Status = true, Type = InstallationHub.Pipenv };
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("[ERROR] Pipenv installation fail", e.Message);
            }

            return new InstallationResult { Status = false, Type = InstallationHub.Pipenv };

        }

    }
}
