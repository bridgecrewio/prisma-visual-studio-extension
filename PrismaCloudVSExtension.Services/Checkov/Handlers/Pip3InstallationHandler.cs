using PrismaCloudVSExtension.Entities.Checkov;
using PrismaCloudVSExtension.Entities.Hubs;
using PrismaCloudVSExtension.Services.Contracts.Common;
using PrismaCloudVSExtension.Services.Contracts.Installation;
using PrismaCloudVSExtension.Utils.Commands;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace PrismaCloudVSExtension.Services.Checkov
{
    public class Pip3InstallationHandler : InstallationAbstractHandler, IPip3InstallationHandler
    {

        public Pip3InstallationHandler(IConfigService configService) : base(configService)
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
            Debug.WriteLine("[DEBUG] Pip3 installation process start");

            try
            {
                bool isPythonVersionSuitable = await _isPythonVersionSuitable("python3", "--version");
                if (isPythonVersionSuitable == true)
                {
                    await AsyncExec.run("pip3", "install --user -U -i https://pypi.org/simple/ checkov");
                    return new InstallationResult { Status = true, Type = InstallationHub.Pip3, Entrypoint = await _resolveEntrypoint() };
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("[ERROR] Pip3 installation fail", e.Message);
            }
            return new InstallationResult { Status = false, Type = InstallationHub.Pip3 };
        }

        private async Task<string> _resolveEntrypoint()
        {
            try
            {
                await AsyncExec.run("checkov", "--version");
                return "checkov";
            }
            catch (Exception e)
            {
                string sitePackagesDirectory = await AsyncExec.run("python3", "-c \"import site; print(site.USER_BASE)\"");

                return Path.Combine(sitePackagesDirectory.Trim(), "bin", "checkov");
            }
        }
    }
}
