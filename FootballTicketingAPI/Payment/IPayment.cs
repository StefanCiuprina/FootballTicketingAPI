using FootballTicketingAPI.IRepositories;
using FootballTicketingAPI.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Payment
{
    public interface IPayment
    {
        Task<bool> Pay(string customerId, TicketBuyModelDetails model, ITicketRepository ticketRepository);
    }
}
