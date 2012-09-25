using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.DeltaApi.Models;
using Ninject;

namespace Codemash.DeltaApi.Controllers
{
    public class SpeakerChangeController : ApiController
    {
        [Inject]
        public ISpeakerChangeRepository SpeakerChangeRepository { get; set; }

        public IEnumerable<SpeakerChangeViewModel> Get()
        {
            return SpeakerChangeRepository.GetAll().Select(CreateSpeakerChangeViewModel);
        }

        public IEnumerable<SpeakerChangeViewModel> Get(int version)
        {
            return SpeakerChangeRepository.GetAll(version).Select(CreateSpeakerChangeViewModel);
        }

        public IEnumerable<SpeakerChangeViewModel> Latest()
        {
            return SpeakerChangeRepository.GetLatest().Select(CreateSpeakerChangeViewModel);
        }

        // private methods
        private static SpeakerChangeViewModel CreateSpeakerChangeViewModel(SpeakerChange speakerChange)
        {
            return new SpeakerChangeViewModel
                       {
                           Action = speakerChange.ActionType.ToString(),
                           SpeakerId = speakerChange.SpeakerId,
                           Key = speakerChange.Key,
                           Value = speakerChange.Value,
                           Version = speakerChange.Version
                       };
        }
    }
}
