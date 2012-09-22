using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Ninject;

namespace Codemash.DeltaApi.Controllers
{
    public class SpeakerController : ApiController
    {
        [Inject]
        public ISpeakerRepository SpeakerRepository { get; set; }

        public IEnumerable<Speaker> Get()
        {
            return SpeakerRepository.GetAll();
        }

        public Speaker Get(int id)
        {
            var result = SpeakerRepository.Get(id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return result;
        }
    }
}
