using FootballTicketingAPI.Models;
using FootballTicketingAPI.Models.FootballClub;
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
    public class FootballClubController : ControllerBase
    {
        private FootballClubService footballClubService;
        public FootballClubController(FootballTicketingContext dbContext)
        {
            footballClubService = new FootballClubService(dbContext);
        }
        // GET: api/<FootballClubController>
        [HttpGet]
        public async Task<IEnumerable<FootballClubModel>> Get()
        {
            return await footballClubService.GetAllFC();
        }

        // GET api/<FootballClubController>/5
        [HttpGet("{id}")]
        public async Task<FootballClubModel> Get(int id)
        {
            return await footballClubService.GetFCById(id);
        }

        // POST api/<FootballClubController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<object> Post(FootballClubInputModel model)
        {
            return resolveResponse(await footballClubService.AddFC(model), model);
        }

        // PUT api/<FootballClubController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<object> Put(FootballClubInputModel model)
        {
            return resolveResponse(await footballClubService.UpdateFC(model), model);
        }

        // DELETE api/<FootballClubController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<object> Delete(int id)
        {
            return resolveResponse(await footballClubService.DeleteFC(id));
        }

        private object resolveResponse(ResponseStatus status, object body = null)
        {
            switch(status)
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
