using FootballTicketingAPI.Models;
using FootballTicketingAPI.Models.Match;
using FootballTicketingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootballTicketingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private MatchService matchService;
        public MatchesController(FootballTicketingContext dbContext)
        {
            matchService = new MatchService(dbContext);
        }
        // GET: api/<MatchesController>
        [HttpGet]
        public async Task<IEnumerable<MatchModel>> Get()
        {
            return await matchService.GetAllMatches();
        }

        // GET api/<MatchesController>/5
        [HttpGet("{id}")]
        public async Task<MatchModel> Get(int id)
        {
            return await matchService.GetMatchById(id);
        }

        // POST api/<MatchesController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<object> Post(MatchInputModel model)
        {
            return resolveResponse(await matchService.AddMatch(model), model);
        }

        // PUT api/<MatchesController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<object> Put(MatchInputModel model)
        {
            return resolveResponse(await matchService.UpdateMatch(model), model);
        }

        // DELETE api/<MatchesController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<object> Delete(int id)
        {
            return resolveResponse(await matchService.DeleteMatch(id));
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
