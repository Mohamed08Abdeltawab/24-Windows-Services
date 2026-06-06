using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace MyFirstWinService
{
    [RunInstaller(true)]
    public partial class ProjectInstallercs : Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller1;
        private ServiceInstaller serviceInstaller1;
        public ProjectInstallercs()
        {
            InitializeComponent();

            serviceProcessInstaller1 = new ServiceProcessInstaller()
            {
                Account = ServiceAccount.LocalSystem
            };

            serviceInstaller1 = new ServiceInstaller()
            {
                ServiceName = "MyFirstWinService",
                DisplayName = "My First Windows Service",
                Description = "A simple Windows Service example.",
                StartType = ServiceStartMode.Manual
            };

            Installers.Add(serviceProcessInstaller1);
            Installers.Add(serviceInstaller1);
        }
    }
}
