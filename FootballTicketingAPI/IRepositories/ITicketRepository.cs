using FootballTicketingAPI.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.IRepositories
{
    public interface ITicketRepository
    {
        Task<List<TicketModel>> GetAllForUser(string id);

        Task<TicketModel> GetById(int id);

        Task<bool> Add(TicketInputModel model);

        Task<bool> Update(TicketInputModel model);

        Task<bool> Delete(int id);
    }
}
