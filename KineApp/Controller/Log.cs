using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;

namespace KineApp.Model
{
    enum LogStatus
    {
        Information,
        Error,
        Critical
    }

    internal class Log
    {

        const string ERRORLOGFILENAME = "Logs\\error.txt";
        const string INFOLOGFILENAME = "Logs\\log.txt";
        const string CRITICALLOGFILENAME = "Logs\\critic.txt";

        /// <summary>
        /// Write text into log file
        /// </summary>
        /// <param name="Line">The line of text to insert into file</param>
        internal static void Write(string line, LogStatus status = LogStatus.Information, bool bypassShutdown = false)
        {
            try
            {
                // Create folder containing log file
                Directory.CreateDirectory("Logs\\");

                string logfile = INFOLOGFILENAME;
                switch(status)
                {
                    case LogStatus.Information:
                        logfile = INFOLOGFILENAME;
                        break; 
                    case LogStatus.Error:
                        logfile = ERRORLOGFILENAME;
                        break; 
                    case LogStatus.Critical:
                        logfile = CRITICALLOGFILENAME;
                        break;
                }

                // Create file if not exists
                if (!File.Exists(logfile)) File.Create(logfile).Close();

                // Append text into file
                using (Stream stream = new FileStream(logfile, FileMode.Open, FileSystemRights.AppendData, FileShare.Write, 4096, FileOptions.WriteThrough))
                {
                    using (TextWriter sw = new StreamWriter(stream))
                    {
                        sw.WriteLine(DateTime.Now + ": " + line);
                        sw.Close();
                    }
                    stream.Close();
                }

                if(status == LogStatus.Critical && !bypassShutdown) {
                    Environment.Exit(0);
                }
            }
            catch (Exception)
            { }
        }
    }
}
