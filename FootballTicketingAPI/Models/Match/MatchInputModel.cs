using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Models.Match
{
    public class MatchInputModel
    {
        public int Id { get; set; }

        public int TeamHomeId { get; set; }

        public int TeamAwayId { get; set; }

        public DateTime DateTime { get; set; }

        public string Stadium { get; set; }

        public int StadiumCapacity { get; set; }

        public double TicketPrice { get; set; }
    }
}
