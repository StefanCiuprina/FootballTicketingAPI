using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.TicketGeneration
{
    // writes log events to a local file
    public class ObserverLogToFile : ILog
    {
        private string fileName;
        public ObserverLogToFile(string fileName)
        {
            this.fileName = fileName;
        }

        public void Log(object sender, LogEventArgs e)
        {
            string message = e.ToString();
            FileStream fileStream;

            Directory.CreateDirectory((new FileInfo(fileName)).DirectoryName);
            fileStream = new FileStream(fileName, FileMode.Create);

            var writer = new StreamWriter(fileStream);
            try
            {
                writer.WriteLine(message);
            }
            catch {}
            finally
            {
                try
                {
                    writer.Close();
                }
                catch {}
            }

        }
    }
}
