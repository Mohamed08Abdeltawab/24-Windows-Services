using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace FileMonitoringService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        ServiceProcessInstaller processInstaller;
        ServiceInstaller serviceInstaller;
        public ProjectInstaller()
        {
            InitializeComponent();

            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "FileMonitoringService",
                DisplayName = "File Monitoring Service",
                Description = "Monitors a specified folder and moves new files to a destination folder.",
                StartType = ServiceStartMode.Automatic,
                ServicesDependedOn = new string[] {"RpcSs", "EventLog", "LanmanWorkstation" } // Example dependencies
            };

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
