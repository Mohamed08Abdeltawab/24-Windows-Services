using System;
using System.IO;
using System.ServiceProcess;
using System.Configuration;

namespace FileMonitoringService
{
    public partial class Service1 : ServiceBase
    {
        private FileSystemWatcher _watcher;
        private readonly string source = ConfigurationManager.AppSettings["SourceFolder"];
        private readonly string destination = ConfigurationManager.AppSettings["DestinationFolder"];
        private readonly string logFile = ConfigurationManager.AppSettings["LogPath"];

        public Service1() => InitializeComponent();

        protected override void OnStart(string[] args)
        {
            // Ensure the source directory exists before starting the watcher
            if (!Directory.Exists(source))
            {
                throw new DirectoryNotFoundException($"Source folder not found: {source}");
            }

            _watcher = new FileSystemWatcher(source);
            _watcher.Created += OnCreated;
            _watcher.EnableRaisingEvents = true;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            // Check if the file is fully written and accessible
            if (!IsFileReady(e.FullPath)) return;

            // Generate a unique file name using GUID and preserve the original extension
            string destinationPath = Path.Combine(destination, Guid.NewGuid().ToString() + Path.GetExtension(e.Name));

            try
            {
                // Move the file to the destination
                File.Move(e.FullPath, destinationPath);
                WriteLog($"Successfully moved: {e.Name} to {destinationPath}");
            }
            catch (Exception ex)
            {
                // Log any errors encountered during the move process
                WriteLog($"Error processing {e.Name}: {ex.Message}");
            }
        }

        // Helper method to verify if a file is locked by another process
        private bool IsFileReady(string filePath)
        {
            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return fs.Length > 0;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }

        // Centralized logging method
        private void WriteLog(string message)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}{Environment.NewLine}";
            File.AppendAllText(logFile, logEntry);
        }

        protected override void OnStop()
        {
            // Properly clean up resources when the service stops
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;
                _watcher.Dispose();
            }
        }
    }
}