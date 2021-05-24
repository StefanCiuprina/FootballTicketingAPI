using FootballTicketingAPI.IRepositories;
using FootballTicketingAPI.Models.Ticket;
using FootballTicketingAPI.TicketGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Payment
{
    public class PaymentOther : IPayment
    {
        public Task<bool> Pay(string customerId, TicketBuyModelDetails model, ITicketRepository ticketRepository)
        {
            if(format(model.PaymentType) == PaymentTypes.TRANSFER)
            {
                Logger.Instance.TransferNotice(model.MatchTitle, model.Date, model.Price);
                return Task.FromResult(true);
            } else if(format(model.PaymentType) == PaymentTypes.CASH)
            {
                Logger.Instance.CashNotice(model.MatchTitle, model.Date, model.Price);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        private PaymentTypes format(string paymentTypeStr)
        {
            switch(paymentTypeStr)
            {
                case "card":
                    return PaymentTypes.CARD;
                case "transfer":
                    return PaymentTypes.TRANSFER;
                case "cash":
                    return PaymentTypes.CASH;
                default:
                    return PaymentTypes.INVALID;
            }
        }
    }
}
