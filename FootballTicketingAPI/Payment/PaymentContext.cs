using FootballTicketingAPI.IRepositories;
using FootballTicketingAPI.Models.Ticket;
using FootballTicketingAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Payment
{
    public class PaymentContext
    {
        IPayment _payment;
        ITicketRepository _ticketRepository;

        public PaymentContext(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public void setPayment(IPayment payment)
        {
            _payment = payment;
        }

        public Task<bool> executePayment(string customerId, TicketBuyModelDetails model)
        {
            return _payment.Pay(customerId, model, _ticketRepository);
        }
    }
}
