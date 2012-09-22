using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Ninject;

namespace Codemash.DeltaApi.Controllers
{
    public class SessionController : ApiController
    {
        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        public IEnumerable<Session> Get()
        {
            return SessionRepository.GetAll();
        }

        public Session Get(int id)
        {
            var result = SessionRepository.Get(id);
            if (result == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return result;
        }
    }
}
