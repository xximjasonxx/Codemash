using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.DeltaApi.Models;
using Ninject;

namespace Codemash.DeltaApi.Controllers
{
    public class SessionChangeController : ApiController
    {
        [Inject]
        public ISessionChangeRepository SessionChangeRepository { get; set; }

        public IEnumerable<SessionChangeViewModel> Get()
        {
            return SessionChangeRepository.GetAll().Select(CreateSessionChangeViewModel);
        }

        public IEnumerable<SessionChangeViewModel> Latest()
        {
            return SessionChangeRepository.GetLatest().Select(CreateSessionChangeViewModel);
        }

        public IEnumerable<SessionChangeViewModel> Get(int version)
        {
            return SessionChangeRepository.GetAll(version).Select(CreateSessionChangeViewModel);
        }

        // private helper methods
        private static SessionChangeViewModel CreateSessionChangeViewModel(SessionChange sessionChange)
        {
            var result = new SessionChangeViewModel
                             {
                                 Action = sessionChange.ActionType.ToString(),
                                 SessionId = sessionChange.SessionId,
                                 Version = sessionChange.Version
                             };

            if (sessionChange.ActionType == ChangeAction.Modify)
            {
                result.Key = sessionChange.Key;
                result.Value = sessionChange.Value;
            }

            return result;
        }
    }
}
