using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace DependenciesService
{
    [RunInstaller(true)]
    public partial class DependenciesServiceInstall : Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;
        public DependenciesServiceInstall()
        {
            processInstaller = new ServiceProcessInstaller()
            {
                Account = ServiceAccount.LocalSystem
            };

            serviceInstaller = new ServiceInstaller()
            {
                ServiceName = "DependenciesService",
                DisplayName = "Dependencies Service",
                Description = "A service that demonstrates dependencies.",
                ServicesDependedOn = new string[] { "EventLog", "RpcSs" },
                StartType = ServiceStartMode.Automatic
            };
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
