using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Codemash.Api.Data.Repositories;
using Ninject;

namespace Codemash.DeltaApi.Controllers
{
    public class SessionsController : ApiController
    {
        [Inject]
        public ISessionRepository SessionRepository { get; set; }
    }
}
