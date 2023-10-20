using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace PrismaCloudVSExtension.Entities.Options
{
    public class PrismaCloudOptions : DialogPage
    {
        [Category("Prisma Cloud")]
        [DisplayName("1. Access Key (Required)")]
        [Description("Prisma Cloud Access Key")]
        public string OptionAccessKey { get; set; }

        [Category("Prisma Cloud")]
        [DisplayName("2. Secret Key (Required)")]
        [Description("Prisma Cloud Secret Key")]
        public string OptionSecretKey { get; set; }

        [Category("Prisma Cloud")]
        [DisplayName("3. Prisma URL (Required)")]
        [Description("Prisma Cloud URL")]
        public string OptionPrismaURL { get; set; }

        [Category("Prisma Cloud")]
        [DisplayName("4. CA-Certificate")]
        [Description("Prisma Cloud CA-Certificate")]
        public string OptionCertificate { get; set; }
    }
}
