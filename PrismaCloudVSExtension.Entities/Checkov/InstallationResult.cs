using PrismaCloudVSExtension.Entities.Hubs;

namespace PrismaCloudVSExtension.Entities.Checkov
{
    public class InstallationResult
    {
        public InstallationHub Type { get; set; }

        public string Entrypoint { get; set; }

        public bool Status { get; set; }
    }
}
