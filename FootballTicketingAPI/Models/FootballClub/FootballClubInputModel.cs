using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Models.FootballClub
{
    public class FootballClubInputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BadgeURL { get; set; }
    }
}
