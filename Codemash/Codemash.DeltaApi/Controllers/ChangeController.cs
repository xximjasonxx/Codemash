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
            return ChangeRepository.GetAll().Select(CreateChangeViewModel);
        }

        public IEnumerable<ChangeViewModel> Get(string channel)
        {
            return ChangeRepository.GetChangesForChannel(channel).Select(CreateChangeViewModel);
        }

        // private helper methods
        private static ChangeViewModel CreateChangeViewModel(Change change)
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
