using System.ComponentModel;
using System.ServiceProcess;

namespace MonitorService
{
    [RunInstaller(true)]
    public class MonitorInstaller : System.Configuration.Install.Installer
    {
        ServiceProcessInstaller processinst;
        ServiceInstaller serviceinst;
        internal static ServiceController ctr;

        public MonitorInstaller()
        {
            //InitializeComponent();
            ctr = new ServiceController();
            processinst = new ServiceProcessInstaller();
            processinst.Account = ServiceAccount.LocalSystem;

            serviceinst = new ServiceInstaller();
            serviceinst.ServiceName = "[Site Monitor]";
            serviceinst.Description = "Site Monitor By Dmytro Smishchenko";
            serviceinst.StartType = ServiceStartMode.Automatic;

            base.Installers.Add(processinst);
            base.Installers.Add(serviceinst);

        }

    }
}
