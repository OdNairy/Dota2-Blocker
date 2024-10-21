using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

[RunInstaller(true)]
public class ProjectInstaller : Installer
{
    private ServiceProcessInstaller processInstaller;
    private ServiceInstaller serviceInstaller;

    public ProjectInstaller()
    {
        processInstaller = new ServiceProcessInstaller();
        serviceInstaller = new ServiceInstaller();

        // Service will run under system account
        processInstaller.Account = ServiceAccount.LocalSystem;

        // Service Information
        serviceInstaller.ServiceName = "Dota2TerminatorService";
        serviceInstaller.DisplayName = "Dota2 Terminator Service";
        serviceInstaller.Description = "Terminates dota2.exe process whenever it appears.";
        serviceInstaller.StartType = ServiceStartMode.Automatic;

        // Add installers to collection. Order is not important.
        Installers.Add(serviceInstaller);
        Installers.Add(processInstaller);
    }
}
