using FootballTicketingAPI.Models;
using FootballTicketingAPI.Models.Ticket;
using FootballTicketingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootballTicketingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private TicketService ticketService;
        private FootballTicketingContext context;
        public TicketController(FootballTicketingContext dbContext)
        {
            ticketService = new TicketService(dbContext);
            context = dbContext;
        }
        // GET: api/<TicketController>
        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IEnumerable<TicketModel>> Get()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return await ticketService.GetAllTicketsForUser(userId);
        }

        // GET: api/<TicketController>
        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("PaymentTypes")]
        public string[] GetPaymentTypes()
        {
            return new string[] { "card", "transfer", "cash" };
        }

        // POST api/<TicketController>
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [Route("BuyTicket")]
        public async Task<object> Post(TicketBuyModel model)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return resolveResponse(await ticketService.BuyTicket(userId, model), model);
        }

        private object resolveResponse(ResponseStatus status, object body = null)
        {
            switch (status)
            {
                case ResponseStatus.OK:
                    return body;
                case ResponseStatus.BAD_REQUEST:
                    return BadRequest();
                case ResponseStatus.NOT_FOUND:
                    return NotFound();
                case ResponseStatus.NO_CONTENT:
                    return NoContent();
                default:
                    return BadRequest();
            }
        }
    }
}
