using FootballTicketingAPI.Models.FootballClub;
using FootballTicketingAPI.Models.Ticket;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Models.Match
{
    public class MatchModel
    {
        [Key]
        public int Id { get; set; }

        [Column()]
        public int TeamHomeId { get; set; }

        [Column()]
        public int TeamAwayId { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public DateTime DateTime { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Stadium { get; set; }

        [Column()]
        public int StadiumCapacity { get; set; }

        [Column()]
        public double TicketPrice { get; set; }


        [ForeignKey("TeamHomeId")]
        public virtual FootballClubModel TeamHome { get; set; }

        [ForeignKey("TeamAwayId")]
        public virtual FootballClubModel TeamAway { get; set; }

        public virtual ICollection<TicketModel> Tickets { get; set; }
    }
}
