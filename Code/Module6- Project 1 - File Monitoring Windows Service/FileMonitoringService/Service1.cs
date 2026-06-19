using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FileMonitoringService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private FileSystemWatcher _watcher;
        //get value of pathes
        string sourcePath = ConfigurationManager.AppSettings["SourceFolder"];
        string destinationPath = ConfigurationManager.AppSettings["DestinationFolder"];
        string logFilePath = ConfigurationManager.AppSettings["LogPath"];


        protected override void OnStart(string[] args)
        {
            //soruce path is file watcher
            _watcher = new FileSystemWatcher(sourcePath);

            //subscrib in event OnCreate

            //enable raising event = true
        }

        protected override void OnStop()
        {
            //disable raising event = false

            //dispose object
        }
    }
}
