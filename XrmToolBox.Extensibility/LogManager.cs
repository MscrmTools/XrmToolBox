using System;
using System.Diagnostics;
using System.IO;
using McTools.Xrm.Connection;

namespace XrmToolBox.Extensibility
{
    public class LogManager
    {
        private ConnectionDetail _connection;
        private readonly string _filePath;

        private enum Level
        {
            Info,
            Warning,
            Error
        }

        /// <summary>
        /// Initialize a new instance of class <see cref="SettingsManager"/>
        /// </summary>
        /// <param name="pluginType">Type of the plugin (used for creating file name)</param>
        /// <param name="connection">Details of the current connection (used for creating file name)</param>
        public LogManager(Type pluginType, ConnectionDetail connection = null)
        {
            _connection = connection;

            _filePath = Path.Combine(Paths.LogsPath, $"{pluginType.Assembly.FullName.Split(',')[0]}.log");
        }

        public string FilePath => _filePath;

        /// <summary>
        /// Set the new connection details
        /// </summary>
        /// <param name="connectionDetail">Details of the connection</param>
        public void SetConnection(ConnectionDetail connectionDetail)
        {
            _connection = connectionDetail;
        }

        /// <summary>
        /// Writes a message in a text file
        /// </summary>
        /// <param name="level">Level of the message</param>
        /// <param name="message">Content of the message</param>
        private void Log(Level level, string message)
        {
            var parentFolder = Path.GetDirectoryName(_filePath);
            if (parentFolder != null && !Directory.Exists(parentFolder))
            {
                Directory.CreateDirectory(parentFolder);
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(_filePath, true))
                {
                    writer.WriteLine("{0}\t{1}\t{2}\t{3}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), _connection?.ConnectionName, level, message);
                }
            }
            catch (Exception error)
            {
                throw new Exception("Unable to write log for the following reason: " + error.Message, error);
            }
        }

        /// <summary>
        /// Writes an information message in the log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Message parameters</param>
        public void LogInfo(string message, params object[] args)
        {
            Log(Level.Info, string.Format(message, args));
        }

        /// <summary>
        /// Writes a warning message in the log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Message parameters</param>
        public void LogWarning(string message, params object[] args)
        {
            Log(Level.Warning, string.Format(message, args));
        }

        /// <summary>
        /// Writes an error message in the log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Message parameters</param>
        public void LogError(string message, params object[] args)
        {
            Log(Level.Error, string.Format(message, args));
        }

        /// <summary>
        /// Opens the log file
        /// </summary>
        public void OpenLog()
        {
            if (File.Exists(_filePath))
            {
                Process.Start(_filePath);
            }
        }

        /// <summary>
        /// Opens the log folder
        /// </summary>
        public void OpenLogFolder()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Process.Start(Path.GetDirectoryName(_filePath));
        }

        /// <summary>
        /// Deletes Log file
        /// </summary>
        public void DeleteLog()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}