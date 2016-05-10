using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using PottyTrainer.DataModel;

namespace PottyTrainer.Api.Controllers
{
    [Route("api/[controller]")]
    public class PottyTrainerController : Controller
    {
        IPottyTrainerRepository _Repository;
        public PottyTrainerController(IPottyTrainerRepository repository)
        {
            _Repository = repository;
        }


        [HttpGet]
        [Route("events")]
        public List<PeePooEvent> Get()
        {
            return _Repository.GetEvents();
        }

        [HttpGet("events/{id}")]
        public PeePooEvent Get(int id)
        {
            return _Repository.GetEvent(id);
        }

        [HttpPost]
        [Route("events")]
        public IActionResult Post([FromBody]PeePooEvent peePooEvent)
        {
            if (peePooEvent == null)
                return HttpBadRequest("incorrect object format");

            var id = _Repository.SaveEvent(peePooEvent);
            return Ok(id);

        }

        [HttpPut("events/{id}")]
        public IActionResult Put(int id, [FromBody]PeePooEvent peePooEvent)
        {

            if (peePooEvent == null)
                return HttpBadRequest("incorrect object format");

            var evtid = _Repository.SaveEvent(peePooEvent);
            return Ok(true);
        }

        [HttpDelete("events/{id}")]
        public IActionResult Delete(int id)
        {
            var resp = _Repository.DeleteEvent(id);
            return Ok(resp);
        }
    }
}
