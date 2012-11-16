using System;
using System.IO;
using Codemash.Server.Core;

namespace Codemash.Poller
{
    public class Logger
    {
        private static Logger _currentLogger;
        public static Logger Current
        {
            get { return _currentLogger ?? (_currentLogger = new Logger()); }
        }

        private Logger()
        {
            if (!Directory.Exists(Config.DriveRoot + @":\temp\logs"))
                Directory.CreateDirectory(Config.DriveRoot + @":\temp\logs");

            if (File.Exists(Config.DriveRoot + @":\temp\logs\PollerOutput.log"))
            {
                var fileHandle = File.Open(Config.DriveRoot + @":\temp\logs\PollerOutput.log", FileMode.Create, FileAccess.Write);
                fileHandle.Dispose();
            }
        }

        public void LogInformation(string message)
        {
            using (var writer = new StreamWriter(GetFileStream()))
            {
                writer.WriteLine("{0} :: {1}", DateTime.Now, message);
            }
        }

        public void LogException(Exception ex)
        {
            do
            {
                LogInformation("Exception 1");
                LogInformation(ex.Message);

                ex = ex.InnerException;
            } while (ex != null);
        }

        private FileStream GetFileStream()
        {
            return File.Open(Config.DriveRoot + @":\temp\logs\PollerOutput.log", FileMode.Append, FileAccess.Write);
        }
    }
}
