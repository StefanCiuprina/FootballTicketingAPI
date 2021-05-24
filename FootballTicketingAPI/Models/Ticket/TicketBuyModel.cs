using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Models.Ticket
{
    public class TicketBuyModel
    {
        public int MatchId { get; set; }
        public string PaymentType { get; set; }
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
    }
}
