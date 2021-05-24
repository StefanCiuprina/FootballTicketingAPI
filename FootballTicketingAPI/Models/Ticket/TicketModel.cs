using FootballTicketingAPI.Models.Match;
using FootballTicketingAPI.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Models.Ticket
{
    public class TicketModel
    {
        [Key]
        public int Id { get; set; }

        [Column()]
        public int MatchId { get; set; }

        [Column()]
        public string CustomerId { get; set; }


        [ForeignKey("MatchId")]
        public virtual MatchModel Match { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { get; set; }
    }
}
