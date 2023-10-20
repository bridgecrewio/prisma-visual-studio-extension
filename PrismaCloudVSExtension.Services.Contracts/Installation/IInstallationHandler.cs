using PrismaCloudVSExtension.Entities.Checkov;
using System.Threading.Tasks;

namespace PrismaCloudVSExtension.Services.Contracts.Installation
{
    public interface IInstallationHandler
    {
        IInstallationHandler SetNext(IInstallationHandler handler);

        Task<InstallationResult> Handle(InstallationContext installationContext);
    }
}
