using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace PrismaCloudVSExtension.Win.Models.Options
{
    public class PrismaCloudOptions : DialogPage
    {
        private string optionAccessKey = "";
        private string optionSecretKey = "";
        private string optionPrismaURL = "";
        private string optionCertificate = "";

        [Category("Prisma Cloud")]
        [DisplayName("1. Access Key (Required)")]
        [Description("Prisma Cloud Access Key")]
        public string OptionAccessKey
        {
            get { return optionAccessKey; }
            set { optionAccessKey = value; }
        }

        [Category("Prisma Cloud")]
        [DisplayName("2. Secret Key (Required)")]
        [Description("Prisma Cloud Secret Key")]
        public string OptionSecretKey
        {
            get { return optionSecretKey; }
            set { optionSecretKey = value; }
        }

        [Category("Prisma Cloud")]
        [DisplayName("3. Prisma URL (Required)")]
        [Description("Prisma Cloud URL")]
        public string OptionPrismaURL
        {
            get { return optionPrismaURL; }
            set { optionPrismaURL = value; }
        }

        [Category("Prisma Cloud")]
        [DisplayName("4. CA-Certificate")]
        [Description("Prisma Cloud CA-Certificate")]
        public string OptionCertificate
        {
            get { return optionCertificate; }
            set { optionCertificate = value; }
        }
    }
}
