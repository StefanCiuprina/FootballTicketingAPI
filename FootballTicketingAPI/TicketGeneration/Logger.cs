using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.TicketGeneration
{
    public sealed class Logger
    {
        public delegate void LogEventHandler(object sender, LogEventArgs e);
        public event LogEventHandler Log;


        private static readonly Logger instance = new Logger();
        private Logger()
        {

        }

        public static Logger Instance
        {
            get { return instance; }
        }


        public void NewTicket(string matchTitle, string date, string price)
        {
            OnLog(new LogEventArgs(matchTitle, date, price));
        }

        public void TransferNotice(string matchTitle, string date, string price)
        {
            var notice = "You have to transfer the amount required for the ticket to our account for your reserved ticket.\n ";
            OnLog(new LogEventArgs(notice + matchTitle, date, price));
        }

        public void CashNotice(string matchTitle, string date, string price)
        {
            var notice = "You have to come to our cash registers to pay the amount required for your reserved ticket.\n";
            OnLog(new LogEventArgs(notice + matchTitle, date, price));
        }

        public void OnLog(LogEventArgs e)
        {
            if (Log != null)
            {
                Log(this, e);
            }
        }
        public void Attach(ILog observer)
        {
            Log += observer.Log;
        }

        public void Detach(ILog observer)
        {
            Log -= observer.Log;
        }
    }
}
