using FootballTicketingAPI.Models.FootballClub;
using FootballTicketingAPI.Models.Match;
using FootballTicketingAPI.Models.Ticket;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballTicketingAPI.Models;
using FootballTicketingAPI.Models.User;

namespace FootballTicketingAPI.Models
{
    public class FootballTicketingContext : IdentityDbContext
    {
        public FootballTicketingContext(DbContextOptions<FootballTicketingContext> options) : base(options)
        {

        }

        public FootballTicketingContext()
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<FootballClubModel> FootballClub { get; set; }

        public DbSet<MatchModel> Match { get; set; }

        public DbSet<TicketModel> Ticket { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* User */
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Tickets)
                .WithOne(d => d.User);

            /* FootballClub */
            modelBuilder.Entity<FootballClubModel>()
                .HasMany(c => c.MatchesHome)
                .WithOne(d => d.TeamHome)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FootballClubModel>()
                .HasMany(c => c.MatchesAway)
                .WithOne(d => d.TeamAway)
                .OnDelete(DeleteBehavior.Restrict);

            /* Match */
            modelBuilder.Entity<MatchModel>()
                .HasMany(c => c.Tickets)
                .WithOne(d => d.Match);

            base.OnModelCreating(modelBuilder);
        }

    }
}
