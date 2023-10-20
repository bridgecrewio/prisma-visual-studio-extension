using PrismaCloudVSExtension.Entities.Checkov;
using PrismaCloudVSExtension.Entities.Config.Types;

namespace PrismaCloudVSExtension.Entities.Config
{
    public class Config
    {
            public ConfigRequirements Requirements { get; set; }        
            public ConfigCheckov Checkov { get; set; }
            public ConfigCommand Command { get; set; }
            public InstallationResult Installation { get; set; }
    }
}
