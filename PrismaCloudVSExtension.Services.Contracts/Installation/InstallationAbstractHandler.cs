using PrismaCloudVSExtension.Entities.Checkov;
using PrismaCloudVSExtension.Services.Contracts.Common;
using PrismaCloudVSExtension.Utils.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PrismaCloudVSExtension.Services.Contracts.Installation
{
    public abstract class InstallationAbstractHandler : IInstallationHandler
    {
        private IInstallationHandler _handler;
        protected readonly IConfigService _configService;

        public InstallationAbstractHandler(IConfigService configService)
        {
            _configService = configService;
        }

        public async virtual Task<InstallationResult> Handle(InstallationContext context)
        {
            if (_handler == null) return null;

            return await _handler.Handle(context);
        }

        public IInstallationHandler SetNext(IInstallationHandler handler)
        {
            _handler = handler;

            return handler;
        }

        protected async Task<bool> _isPythonVersionSuitable(string command, string options)
        {
            Debug.WriteLine("[DEBUG] Checking the Python version");

            try
            {
                string pythonVersion = (await AsyncExec.run(command, options))?.Split(' ')[1];
                Debug.WriteLine(pythonVersion, "[DEBUG] Existing Python version");
                Debug.WriteLine(_configService.Config.Requirements.MinPythonVersion, "[DEBUG] Required Python version");

                return (new Version(pythonVersion)).CompareTo(new Version(_configService.Config.Requirements.MinPythonVersion)) >= 0;
            }
            catch (Exception e)
            {
                Debug.WriteLine("[ERROR] Checking the Python version fail", e.Message);
            }

            return false;
        }
    }
}
