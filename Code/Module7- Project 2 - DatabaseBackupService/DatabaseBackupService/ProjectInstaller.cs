using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace DatabaseBackupService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        ServiceProcessInstaller processInstaller;
        ServiceInstaller serviceInstaller;
        public ProjectInstaller()
        {
            InitializeComponent();

            processInstaller = new ServiceProcessInstaller()
            {
                Account = ServiceAccount.LocalService
            };

            serviceInstaller = new ServiceInstaller()
            {
                ServiceName = "DatabaseBackup",
                DisplayName = "Database Backup",
                Description = "service to backup database file.",
                ServicesDependedOn = new string[] { "MSSQLSERVER", "RpcSs", "EventLog" },
                StartType = ServiceStartMode.Automatic

            };

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);

        }
    }
}
