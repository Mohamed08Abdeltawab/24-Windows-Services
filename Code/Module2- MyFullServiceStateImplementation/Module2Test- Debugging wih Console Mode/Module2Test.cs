using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Module2Test
{
    public partial class Module2Test : ServiceBase
    {
        private string logDirectory;
        private string logFilePath;
        public Module2Test()
        {
            InitializeComponent();
            CanPauseAndContinue = true;
            CanShutdown = true;

            logDirectory = ConfigurationManager.AppSettings["LogDirectory"];

            if (string.IsNullOrWhiteSpace(logDirectory))
            {
                throw new ConfigurationErrorsException("LogDirectory is not configured in the application settings.");
            }

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            logFilePath = Path.Combine(logDirectory, "ServiceStateLog.txt");
        }




        public void EventLogWrite(string message)
        {
            string messageToWrite = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}\n";
            File.AppendAllText(logFilePath, messageToWrite);

            //for debugging 
            if (Environment.UserInteractive)
            {
                Console.WriteLine(messageToWrite);
            }
        }

        protected override void OnStart(string[] args)
        {
            EventLogWrite("Service started.");
        }

        protected override void OnStop()
        {
            EventLogWrite("Service stopped.");
        }

        protected override void OnPause()
        {
            EventLogWrite("Service paused.");
        }

        protected override void OnContinue()
        {
            EventLogWrite("Service continued.");
        }

        protected override void OnShutdown()
        {
            EventLogWrite("Service shutdown.");
        }

        public void StartInConsole()
        {
            OnStart(null);
            Console.WriteLine("Service started. Press any key to stop...");
            Console.ReadLine();
            OnStop();
            Console.ReadKey();
        }
    }
}
