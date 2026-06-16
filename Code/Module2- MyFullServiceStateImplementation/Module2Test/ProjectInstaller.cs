using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Module2Test
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;
        public ProjectInstaller()
        {
            InitializeComponent();

            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalService
            };

            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "Module2Test",
                DisplayName = "Module2 Test",
                Description = "Creating Services to testing the event of start and stop and pause and resume",
                StartType = ServiceStartMode.Automatic

            };

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }

    }
}
