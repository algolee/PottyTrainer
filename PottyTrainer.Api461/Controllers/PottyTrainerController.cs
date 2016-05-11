using PottyTrainer.DataModel;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PottyTrainer.Api461.Controllers
{

    public class PottyTrainerController : ApiController
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

        [HttpGet]
        [System.Web.Http.Route("events/{id}")]
        public PeePooEvent Get(int id)
        {
            return _Repository.GetEvent(id);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("events")]
        public async Task<string> Post([FromBody]PeePooEvent peePooEvent)
        {
            if (peePooEvent == null)
                throw new HttpResponseException(new HttpResponseMessage { ReasonPhrase = "incorrect object format", StatusCode = HttpStatusCode.BadRequest });

            var id = await _Repository.SaveEvent(peePooEvent);
            return id;

        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("events/{id}")]
        public bool Put(int id, [FromBody]PeePooEvent peePooEvent)
        {

            if (peePooEvent == null)
                throw new HttpResponseException(new HttpResponseMessage { ReasonPhrase = "incorrect object format", StatusCode = HttpStatusCode.BadRequest });

            var evtid = _Repository.SaveEvent(peePooEvent);
            return true;
        }

        [HttpDelete]
        [Route("events/{id}")]
        public async Task<bool> Delete(int id)
        {
            var resp = await _Repository.DeleteEvent(id);
            return resp;
        }
    }
}
