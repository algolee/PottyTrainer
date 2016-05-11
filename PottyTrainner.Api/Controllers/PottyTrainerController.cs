using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Cors;
using Microsoft.AspNet.Cors.Infrastructure;
using Microsoft.AspNet.Mvc;
using PottyTrainer.Contracts;

namespace PottyTrainner.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]
    public class PottyTrainerController : Controller
    {
        [HttpGet]
        [Route("events")]
        public IQueryable<PeePooEvent> Get()
        {
            return null;
        }

        [HttpGet("events/{id}")]
        public PeePooEvent Get(int id)
        {
            return new PeePooEvent() { EventType = EventType.Pee};
        }

        [HttpPost("events")]
        public IActionResult Post([FromBody]PeePooEvent peePooEvent)
        {

            return Ok(100);

        }

        [HttpPut("events/{id}")]
        public int Put(int id, [FromBody]PeePooEvent peePooEvent)
        {
            return 100;
        }

        [HttpDelete("events/{id}")]
        public bool Delete(int id)
        {
            return true;
        }
    }
}
