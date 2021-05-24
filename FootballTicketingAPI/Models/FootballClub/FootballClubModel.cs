using FootballTicketingAPI.Models.Match;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FootballTicketingAPI.Models.User;

namespace FootballTicketingAPI.Models.FootballClub
{
    public class FootballClubModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string BadgeURL { get; set; }

        public virtual ICollection<MatchModel> MatchesHome { get; set; }
        public virtual ICollection<MatchModel> MatchesAway { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as FootballClubModel;

            if (item == null)
            {
                return false;
            }

            bool cond1 = this.Id.Equals(item.Id);
            bool cond2 = this.Name.Equals(item.Name);
            bool cond3 = this.BadgeURL.Equals(item.BadgeURL);
            return cond1 && cond2 && cond3;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
