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

namespace MyFirstWinService
{
    public partial class MyFirstWinService : ServiceBase
    {
        public MyFirstWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string logDirctory = @"C:\Logs";
            string logFilePath = Path.Combine(logDirctory, "MyServiceLog.txt");

            if (!Directory.Exists(logDirctory))
            {
                Directory.CreateDirectory(logDirctory);
            }

            string logMessage = $"Service started at {DateTime.Now}";
            File.AppendAllText(logFilePath, logMessage);
        }

        protected override void OnStop()
        {
            string logDirctory = @"C:\Logs";
            string logFilePath = Path.Combine(logDirctory, "MyServiceLog.txt");

            if (!Directory.Exists(logDirctory))
            {
                Directory.CreateDirectory(logDirctory);
            }

            string logMessage = $"Service stopped at {DateTime.Now}";
            File.AppendAllText(logFilePath, logMessage);
        }
    }
}
