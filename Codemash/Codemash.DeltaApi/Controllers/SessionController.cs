using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.DeltaApi.Models;
using Codemash.Server.Core.Extensions;
using Ninject;

namespace Codemash.DeltaApi.Controllers
{
    public class SessionController : ApiController
    {
        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        public IEnumerable<SessionViewModel> Get()
        {
            return SessionRepository.GetAll().Select(CreateSessionViewModel);
        }

        public SessionViewModel Get(long id)
        {
            var result = SessionRepository.Get(id);
            if (result == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return CreateSessionViewModel(result);
        }

        // private helper methods
        private static SessionViewModel CreateSessionViewModel(Session session)
        {
            return new SessionViewModel
                       {
                           Abstract = session.Abstract,
                           End = session.End.AsDateTimeDisplay(),
                           Level = session.Level,
                           Room = session.Room,
                           SessionId = session.SessionId,
                           SpeakerId = session.SpeakerId,
                           Start = session.Start.AsDateTimeDisplay(),
                           Title = session.Title,
                           Track = session.Track
                       };
        }
    }
}
