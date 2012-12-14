using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.DeltaApi.Models;
using Codemash.Notification.Manager;
using Codemash.Server.Core.Ex;
using Ninject;

namespace Codemash.DeltaApi.Controllers
{
    public class ChangeController : ApiController
    {
        [Inject]
        public IChangeRepository ChangeRepository { get; set; }

        [Inject]
        public IClientRepository ClientRepository { get; set; }

        public IEnumerable<ChangeViewModel> Get()
        {
            return ChangeRepository.GetAll().Select(CreateChangeViewModel);
        }

        public IEnumerable<ChangeViewModel> Get(string channel)
        {
            try
            {
                return ChangeRepository.GetChangesForChannel(channel).Select(CreateChangeViewModel);
            }
            catch (ClientNotFoundException)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }

        public IEnumerable<ChangeViewModel> Get(int clientId)
        {
            try
            {
                return ChangeRepository.GetChangesForClient(clientId).Select(CreateChangeViewModel);
            }
            catch (ClientNotFoundException)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }
            
        [HttpPost]
        public HttpStatusCode Update(ClientUpdateModel model)
        {
            var client = ClientRepository.Get(model.ChannelUri);
            if (client != null)
            {
                // update the client
                ClientRepository.UpdateClientChangeset(client.ChannelUri, model.Changeset);
            }

            return HttpStatusCode.OK;
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

            if (change.Action != ChangeAction.Delete.ToString())
            {
                result.Key = change.Key;
                result.Value = change.Value;
            }

            return result;
        }
    }
}
