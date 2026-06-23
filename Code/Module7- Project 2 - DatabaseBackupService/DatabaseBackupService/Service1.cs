using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;

namespace DatabaseBackupService
{
    public partial class Service1 : ServiceBase
    {
        Timer _timer;
        private string LogFolderPath;
        private string BackupFolderPath;
        private string connString;
        private int BackupIntervalMinutes;


        private string DatabaseFilePath;
        public Service1()
        {
            InitializeComponent();

            LogFolderPath = ConfigurationManager.AppSettings["LogFolder"];
            BackupFolderPath = ConfigurationManager.AppSettings["BackupFolder"];
            connString = ConfigurationManager.ConnectionStrings["MyDbConn"].ConnectionString;
            DatabaseFilePath = connString.Split(';')[0].Replace("Data Source=", "").Trim();

            BackupIntervalMinutes = int.TryParse(ConfigurationManager.AppSettings["BackupIntervalMinutes"], out int result) ? result : 60; // Default to 60 minutes if not specified

            Directory.CreateDirectory(LogFolderPath);
            Directory.CreateDirectory(BackupFolderPath);
        }

        protected override void OnStart(string[] args)
        {
            Logs("Service Started.");
            _timer = new Timer(
                callback: PerformBackup,
                state: null,
                dueTime: TimeSpan.Zero,
                period: TimeSpan.FromMinutes(BackupIntervalMinutes)
            );
            Logs($"Backup interval set to {BackupIntervalMinutes} minutes.");
        }

        public void PerformBackup(object state)
        {
            try
            {
                if (!Directory.Exists(BackupFolderPath))
                {
                    Directory.CreateDirectory(BackupFolderPath);
                }

                string backupFileName = Path.Combine(BackupFolderPath, $"Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db");

                // الآن نستخدم المسار الصحيح للملف
                File.Copy(DatabaseFilePath, backupFileName, true);

                Logs($"Backup completed successfully at: {backupFileName}");
            }
            catch (Exception ex)
            {
                Logs($"Error during backup: {ex.Message}");
            }
        }


        protected override void OnStop()
        {
            _timer?.Dispose();
            Logs("Service Stopped.");
        }

        public void Logs(string message )
        {
            string logFilePath = Path.Combine(LogFolderPath, "DatabaseBackupService.txt");
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}
