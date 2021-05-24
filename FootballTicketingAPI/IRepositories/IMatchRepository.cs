using FootballTicketingAPI.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.IRepositories
{
    public interface IMatchRepository
    {
        Task<List<MatchModel>> GetAll();

        Task<MatchModel> GetById(int id);

        Task<bool> Add(MatchInputModel model);

        Task<bool> Update(MatchInputModel model);

        Task<bool> Delete(int id);

        Task<bool> MatchExistsById(int id);
    }
}
