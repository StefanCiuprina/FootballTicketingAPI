using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballTicketingAPI.IRepositories;
using FootballTicketingAPI.Models;
using FootballTicketingAPI.Models.FootballClub;
using Microsoft.EntityFrameworkCore;

namespace FootballTicketingAPI.Repositories
{
    public class FootballClubRepository : IFootballClubRepository
    {
        protected readonly FootballTicketingContext _context;

        public FootballClubRepository(FootballTicketingContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> Add(FootballClubInputModel inputModel)
        {
            var model = new FootballClubModel
            {
                Name = inputModel.Name,
                BadgeURL = inputModel.BadgeURL
            };
            _context.FootballClub.Add(model);
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
            var model = await _context.FootballClub.FindAsync(id);
            if (model == null)
            {
                return false;
            }

            _context.FootballClub.Remove(model);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<FootballClubModel>> GetAll()
        {
            return await _context.FootballClub.ToListAsync();
        }

        public async Task<FootballClubModel> GetById(int id)
        {
            return await _context.FootballClub.FindAsync(id);
        }

        public async Task<bool> Update(FootballClubInputModel newModel)
        {
            var model = await _context.FootballClub.FindAsync(newModel.Id);
            if (model == null)
            {
                return false;
            }

            model.Name = newModel.Name;
            model.BadgeURL = newModel.BadgeURL;

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
