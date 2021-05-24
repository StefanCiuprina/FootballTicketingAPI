using FootballTicketingAPI.Models.FootballClub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.IRepositories
{
    public interface IFootballClubRepository
    {
        Task<List<FootballClubModel>> GetAll();

        Task<FootballClubModel> GetById(int id);

        Task<bool> Add(FootballClubInputModel model);

        Task<bool> Update(FootballClubInputModel model);

        Task<bool> Delete(int id);
    }
}
