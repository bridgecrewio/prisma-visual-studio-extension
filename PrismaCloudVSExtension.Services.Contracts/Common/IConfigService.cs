using PrismaCloudVSExtension.Entities.Config;
using System;

namespace PrismaCloudVSExtension.Services.Contracts.Common
{
    public interface IConfigService
    {
        Config Config { get; set; }
    }
}
