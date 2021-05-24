using FootballTicketingAPI.IRepositories;
using FootballTicketingAPI.Models;
using FootballTicketingAPI.Models.Ticket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        protected readonly FootballTicketingContext _context;

        public TicketRepository(FootballTicketingContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> Add(TicketInputModel inputModel)
        {
            var model = new TicketModel
            {
                Id = inputModel.Id,
                MatchId = inputModel.MatchId,
                CustomerId = inputModel.CustomerId
            };
            _context.Ticket.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var model = await _context.Ticket.FindAsync(id);
            if (model == null)
            {
                return false;
            }

            _context.Ticket.Remove(model);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TicketModel>> GetAllForUser(string id)
        {
            return await _context.Ticket.Where(ticket => ticket.CustomerId == id).ToListAsync();
        }

        public async Task<TicketModel> GetById(int id)
        {
            return await _context.Ticket.FindAsync(id);
        }

        public async Task<bool> Update(TicketInputModel newModel)
        {
            var model = await _context.Ticket.FindAsync(newModel.Id);
            if (model == null)
            {
                return false;
            }

            model.MatchId = newModel.MatchId;
            model.CustomerId = newModel.CustomerId;

            _context.Entry(model).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
