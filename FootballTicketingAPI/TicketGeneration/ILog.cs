using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.TicketGeneration
{
    public interface ILog
    {
        void Log(object sender, LogEventArgs e);
    }
}
