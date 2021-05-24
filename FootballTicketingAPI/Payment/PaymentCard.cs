using FootballTicketingAPI.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballTicketingAPI.Payment.Card;
using FootballTicketingAPI.Repositories;
using FootballTicketingAPI.IRepositories;
using System.Net.Mail;
using FootballTicketingAPI.TicketGeneration;
using System.Net;
using System.IO;

namespace FootballTicketingAPI.Payment
{
    public class PaymentCard : IPayment
    {
        public async Task<bool> Pay(string customerId, TicketBuyModelDetails buyModel, ITicketRepository ticketRepository)
        {
            var creditCard = new CreditCard()
            {
                NameOnCard = buyModel.NameOnCard,
                CardNumber = buyModel.CardNumber,
                ExpirationDate = buyModel.ExpirationDate,
                SecurityCode = buyModel.SecurityCode
            };

            bool isValid = CreditCardValidator.Validate(creditCard);

            if(isValid)
            {
                
                Logger.Instance.NewTicket(buyModel.MatchTitle, buyModel.Date, buyModel.Price);

                var ticketModel = new TicketInputModel()
                {
                    Id = 0,
                    MatchId = buyModel.MatchId,
                    CustomerId = customerId
                };

                return await ticketRepository.Add(ticketModel);
                return true;
            } else
            {
                return false;
            }
        }
    }
    
}
