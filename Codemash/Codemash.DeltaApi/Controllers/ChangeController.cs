using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.DeltaApi.Models;
using Ninject;

namespace Codemash.DeltaApi.Controllers
{
    public class ChangeController : ApiController
    {
        [Inject]
        public IChangeRepository ChangeRepository { get; set; }

        public IEnumerable<ChangeViewModel> Get()
        {
            return ChangeRepository.GetLatest().Select(CreateSessionChangeViewModel);
        }

        public IEnumerable<ChangeViewModel> Get(int id)
        {
            return ChangeRepository.GetAll(id).Select(CreateSessionChangeViewModel);
        }

        // private helper methods
        private static ChangeViewModel CreateSessionChangeViewModel(Change change)
        {
            var result = new ChangeViewModel
                             {
                                 Changeset = change.Changeset,
                                 Action = change.Action,
                                 EntityId = change.EntityId,
                                 EntityType = change.EntityType
                             };

            if (change.Action == ChangeAction.Modify.ToString())
            {
                result.Key = change.Key;
                result.Value = change.Value;
            }

            return result;
        }
    }
}
