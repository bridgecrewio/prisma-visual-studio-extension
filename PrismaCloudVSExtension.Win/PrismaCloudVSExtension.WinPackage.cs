using Microsoft.VisualStudio.Shell;
using PrismaCloudVSExtension.Services.Checkov;
using PrismaCloudVSExtension.Entities.Options;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;
using PrismaCloudVSExtension.Entities.Checkov;
using System.Diagnostics;
using Autofac;

namespace PrismaCloudVSExtension.Win
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PrismaCloudVSExtensionWinPackage.PackageGuidString)]
    [ProvideOptionPage(typeof(PrismaCloudOptions), "Prisma Cloud", "General", 0, 0, true)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(PrismaCloudVSExtension.Win.Properties.ToolWindow1))]
    public sealed class PrismaCloudVSExtensionWinPackage : AsyncPackage
    {

        public IContainer _container { get; set; }


        public PrismaCloudVSExtensionWinPackage() : base()
        {
            _container = Bootstrap.GetContainer();
           
            Debug.WriteLine("[DEBUG] Init package");
        }

        /// <summary>
        /// PrismaCloudVSExtension.WinPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "806546d0-e1ea-4119-8910-31088cb0d1ab";

    #region Package Members

    /// <summary>
    /// Initialization of the package; this method is called right after the package is sited, so this is the place
    /// where you can put all the initialization code that rely on services provided by VisualStudio.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
    /// <param name="progress">A provider for progress updates.</param>
    /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
        Debug.WriteLine("[DEBUG] Initialize package");

        // When initialized asynchronously, the current thread may be a background thread at this point.
        // Do any initialization that requires the UI thread after switching to the UI thread.
        await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
        await PrismaCloudVSExtension.Win.Properties.ToolWindow1Command.InitializeAsync(this);
        await _container.Resolve<CheckovInstallationService>().Install(new InstallationContext());
    }

    #endregion
}
}
