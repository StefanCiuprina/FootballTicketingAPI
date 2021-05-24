using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Models.Ticket
{
    public class TicketInputModel
    {
        public int Id { get; set; }

        public int MatchId { get; set; }

        public string CustomerId { get; set; }
    }
}
