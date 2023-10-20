using Newtonsoft.Json;
using PrismaCloudVSExtension.Entities.Config;
using PrismaCloudVSExtension.Services.Contracts.Common;
using System.IO;


namespace PrismaCloudVSExtension.Services.Common
{
    public class ConfigService : IConfigService
    {
        public Config Config { get; set; }

        public ConfigService() 
        {
            string path = @Path.Combine(Directory.GetCurrentDirectory(), "../..", "config.json");
            var jsonConfig = File.ReadAllText(path);

            Config = JsonConvert.DeserializeObject<Config>(jsonConfig); 
        }
    }
}
