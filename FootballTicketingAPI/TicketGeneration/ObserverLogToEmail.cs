using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FootballTicketingAPI.TicketGeneration
{
    // sends log events via email.
    public class ObserverLogToEmail : ILog
    {
        private string from;
        private string to;
        private string subject;
        private string body;
        private SmtpClient smtpClient;

        public ObserverLogToEmail(string from, string to, SmtpClient smtpClient)
        {
            this.from = from;
            this.to = to;
            this.subject = "Match Ticket";

            this.smtpClient = smtpClient;
        }

        #region ILog Members
        public void Log(object sender, LogEventArgs e)
        {
            body = e.ToString();
            smtpClient.Send(from, to, subject, body);
        }
        #endregion
    }
}
