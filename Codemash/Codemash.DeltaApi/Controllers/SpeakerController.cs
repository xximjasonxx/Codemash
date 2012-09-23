using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.DeltaApi.Models;
using Ninject;

namespace Codemash.DeltaApi.Controllers
{
    public class SpeakerController : ApiController
    {
        [Inject]
        public ISpeakerRepository SpeakerRepository { get; set; }

        public IEnumerable<SpeakerViewModel> Get()
        {
            return SpeakerRepository.GetAll().Select(CreateSpeakerViewModel);
        }

        public SpeakerViewModel Get(int id)
        {
            var result = SpeakerRepository.Get(id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return CreateSpeakerViewModel(result);
        }

        // private methods
        private static SpeakerViewModel CreateSpeakerViewModel(Speaker speaker)
        {
            return new SpeakerViewModel
                       {
                           Biography = speaker.Biography,
                           BlogUrl = speaker.BlogUrl,
                           Company = speaker.Company,
                           EmailAddress = speaker.EmailAddress,
                           FirstName = speaker.FirstName,
                           LastName = speaker.LastName,
                           SpeakerId = speaker.SpeakerId,
                           Twitter = speaker.Twitter
                       };
        }
    }
}
