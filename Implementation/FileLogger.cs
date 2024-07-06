using Implementation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class FileLogger : IFileLogger
    {

        public void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        public void LogError(string message, Exception ex)
        {
            Log("ERROR", $"{message}. Exception: {ex.Message}");
        }

        private void Log(string logLevel, string message)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}";
            try
            {
                File.AppendAllText(Constantes.LOG_FILE_PATH, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Handle potential issues with logging (e.g., file access issues)
                Console.WriteLine($"Failed to log message to file: {ex.Message}");
            }
        }
    }
}
