using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.TicketGeneration
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string matchTitle, string date, string price)
        {
            MatchTitle = matchTitle;
            Date = date;
            Price = price;
        }
        public string MatchTitle { get; private set; }
        public string Date { get; private set; }
        public string Price { get; private set; }
        public override String ToString()
        {
            return "" + MatchTitle + "\n" + Date + "\n" + Price;
        }
    }
}
