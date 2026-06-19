using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.IO;

namespace FaultHandlingService
{
    public partial class MyCustomService : ServiceBase
    {
        private string logFilePath = @"C:\ServiceLogs\RecoveryLog.txt";
        public MyCustomService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogEvent("Service Started");
            Thread workerThread = new Thread(WorkerTask);
            workerThread.Start();
        }

        protected override void OnStop()
        {
            LogEvent("Service Stoped.");
        }

        public void WorkerTask()
        {
            //using try catch to error handling in services
            try
            {
                while (true)
                {
                    LogEvent("Service is running...");
                    Thread.Sleep(1000);

                    throw new Exception("Simulated Error for testing recovery");
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1);
            }
        }

        public void LogEvent(string message)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }

        public static void Main(string[] args)
        {
            ServiceBase.Run(new MyCustomService());
        }
    }
}
