using FootballTicketingAPI.Controllers;
using FootballTicketingAPI.IRepositories;
using FootballTicketingAPI.Models;
using FootballTicketingAPI.Models.Match;
using FootballTicketingAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Services
{
    public class MatchService
    {
        private IMatchRepository matchRepository;

        public MatchService(FootballTicketingContext dbContext)
        {
            matchRepository = new MatchRepository(dbContext);
        }

        public async Task<ResponseStatus> AddMatch(MatchInputModel model)
        {
            bool result = await matchRepository.Add(model);
            if (!result)
            {
                return ResponseStatus.BAD_REQUEST;
            }
            return ResponseStatus.OK;
        }

        public async Task<ResponseStatus> DeleteMatch(int id)
        {
            bool result = await matchRepository.Delete(id);
            if (!result)
            {
                return ResponseStatus.BAD_REQUEST;
            }
            return ResponseStatus.NO_CONTENT;
        }

        public async Task<List<MatchModel>> GetAllMatches()
        {
            return await matchRepository.GetAll();
        }

        public async Task<MatchModel> GetMatchById(int id)
        {
            return await matchRepository.GetById(id);
        }

        public async Task<ResponseStatus> UpdateMatch(MatchInputModel newModel)
        {
            bool result = await matchRepository.Update(newModel);
            if (!result)
            {
                return ResponseStatus.BAD_REQUEST;
            }
            return ResponseStatus.OK;
        }
    }
}
