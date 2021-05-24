using FootballTicketingAPI.Controllers;
using FootballTicketingAPI.IRepositories;
using FootballTicketingAPI.Models;
using FootballTicketingAPI.Models.FootballClub;
using FootballTicketingAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Services
{
    public class FootballClubService
    {
        private IFootballClubRepository footballClubRepository;

        public FootballClubService(FootballTicketingContext dbContext)
        {
            footballClubRepository = new FootballClubRepository(dbContext);
        }

        public async Task<ResponseStatus> AddFC(FootballClubInputModel model)
        {
            bool result = await footballClubRepository.Add(model);
            if(!result)
            {
                return ResponseStatus.BAD_REQUEST;
            }
            return ResponseStatus.OK;
        }

        public async Task<ResponseStatus> DeleteFC(int id)
        {
            bool result = await footballClubRepository.Delete(id);
            if (!result)
            {
                return ResponseStatus.NOT_FOUND;
            }
            return ResponseStatus.NO_CONTENT;
        }

        public async Task<List<FootballClubModel>> GetAllFC()
        {
            return await footballClubRepository.GetAll();
        }

        public async Task<FootballClubModel> GetFCById(int id)
        {
            return await footballClubRepository.GetById(id);
        }

        public async Task<ResponseStatus> UpdateFC(FootballClubInputModel newModel)
        {
            bool result = await footballClubRepository.Update(newModel);
            if (!result)
            {
                return ResponseStatus.NOT_FOUND;
            }
            return ResponseStatus.OK;
        }
    }
}
