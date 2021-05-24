using FootballTicketingAPI.Models.Ticket;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }


        public virtual ICollection<TicketModel> Tickets { get; set; }
    }
}
