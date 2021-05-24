using FootballTicketingAPI.Controllers;
using FootballTicketingAPI.IRepositories;
using FootballTicketingAPI.Models;
using FootballTicketingAPI.Models.Match;
using FootballTicketingAPI.Models.Ticket;
using FootballTicketingAPI.Models.User;
using FootballTicketingAPI.Payment;
using FootballTicketingAPI.Repositories;
using FootballTicketingAPI.TicketGeneration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Services
{
    public class TicketService
    {
        private ITicketRepository ticketRepository;
        private IMatchRepository matchRepository;
        private IFootballClubRepository footballClubRepository;
        private PaymentContext paymentContext;

        public TicketService(FootballTicketingContext dbContext)
        {
            ticketRepository = new TicketRepository(dbContext);
            matchRepository = new MatchRepository(dbContext);
            footballClubRepository = new FootballClubRepository(dbContext);
            paymentContext = new PaymentContext(ticketRepository);
        }

        public async Task<ResponseStatus> BuyTicket(string customerId, TicketBuyModel model)
        {
            if (format(model.PaymentType) == PaymentTypes.CARD)
            {
                paymentContext.setPayment(new PaymentCard());
            }
            else if (format(model.PaymentType) == PaymentTypes.TRANSFER || format(model.PaymentType) == PaymentTypes.CASH)
            {
                paymentContext.setPayment(new PaymentOther());
            }
            else
            {
                return ResponseStatus.BAD_REQUEST;
            }

            if (!(await matchRepository.MatchExistsById(model.MatchId)))
            {
                return ResponseStatus.NOT_FOUND;
            }

            var match = await matchRepository.GetById(model.MatchId);

            var matchTitle = await getMatchTitle(match);
            var matchDate = getMatchDate(match);
            var matchPrice = getMatchPrice(match);

            var ticketDetails = createTicketDetails(matchTitle, matchDate, matchPrice, model);

            InitializeLogger();
            bool result = await paymentContext.executePayment(customerId, ticketDetails);

            if (!result)
            {
                return ResponseStatus.BAD_REQUEST;
            }
            return ResponseStatus.OK;
        }

        public async Task<List<TicketModel>> GetAllTicketsForUser(string id)
        {
            return await ticketRepository.GetAllForUser(id);
        }

        private async Task<string> getMatchTitle(MatchModel match)
        {
            var teamHomeName = (await footballClubRepository.GetById(match.TeamHomeId)).Name;
            var teamAwayName = (await footballClubRepository.GetById(match.TeamAwayId)).Name;
            return teamHomeName + " - " + teamAwayName;
        }

        private string getMatchPrice(MatchModel match)
        {
            return match.TicketPrice.ToString() + " LEI";
        }

        private string getMatchDate(MatchModel match)
        {
            return match.DateTime.ToString("dd-MM-yy");
        }

        private TicketBuyModelDetails createTicketDetails(string matchTitle, string matchDate, string matchPrice, TicketBuyModel model)
        {
            return new TicketBuyModelDetails()
            {
                MatchId = model.MatchId,
                MatchTitle = matchTitle,
                Date = matchDate,
                Price = matchPrice,
                PaymentType = model.PaymentType,
                NameOnCard = model.NameOnCard,
                CardNumber = model.CardNumber,
                ExpirationDate = model.ExpirationDate,
                SecurityCode = model.SecurityCode
            };
        }

        private void InitializeLogger()
        {
            string from = "stefan.ciuprina@gmail.com";
            string to = "stefan.ciuprina@gmail.com";

            string password = readPassword();

            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("stefan.ciuprina@gmail.com", password)
            };
            var logEmail = new ObserverLogToEmail(from, to, smtpClient);
            Logger.Instance.Attach(logEmail);

            var logFile = new ObserverLogToFile(@"C:\Temp\Ticket.txt");
            Logger.Instance.Attach(logFile);
        }

        private string readPassword()
        {
            try
            {
                using (var sr = new StreamReader("passTODELETE.txt"))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                return "";
            }
        }

        private PaymentTypes format(string paymentTypeStr)
        {
            switch (paymentTypeStr)
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
