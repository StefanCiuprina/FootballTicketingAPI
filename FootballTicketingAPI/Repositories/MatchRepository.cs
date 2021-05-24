using FootballTicketingAPI.IRepositories;
using FootballTicketingAPI.Models;
using FootballTicketingAPI.Models.Match;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        protected readonly FootballTicketingContext _context;

        public MatchRepository(FootballTicketingContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> Add(MatchInputModel inputModel)
        {
            var model = new MatchModel
            {
                Id = inputModel.Id,
                TeamHomeId = inputModel.TeamHomeId,
                TeamAwayId = inputModel.TeamAwayId,
                DateTime = inputModel.DateTime,
                Stadium = inputModel.Stadium,
                StadiumCapacity = inputModel.StadiumCapacity,
                TicketPrice = inputModel.TicketPrice
            };
            _context.Match.Add(model);
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
            var model = await _context.Match.FindAsync(id);
            if (model == null)
            {
                return false;
            }

            _context.Match.Remove(model);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<MatchModel>> GetAll()
        {
            return await _context.Match.ToListAsync();
        }

        public async Task<MatchModel> GetById(int id)
        {
            return await _context.Match.FindAsync(id);
        }

        public async Task<bool> Update(MatchInputModel newModel)
        {
            var model = await _context.Match.FindAsync(newModel.Id);
            if (model == null)
            {
                return false;
            }

            model.TeamHomeId = newModel.TeamHomeId;
            model.TeamAwayId = newModel.TeamAwayId;
            model.DateTime = newModel.DateTime;
            model.Stadium = newModel.Stadium;
            model.StadiumCapacity = newModel.StadiumCapacity;
            model.TicketPrice = newModel.TicketPrice;

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

        public async Task<bool> MatchExistsById(int id)
        {
            var match = await _context.Match.FindAsync(id);
            return match != null;
        }
    }
}
