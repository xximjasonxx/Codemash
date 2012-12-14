using System;
using System.Net;
using System.Web.Http;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.DeltaApi.Models;
using Ninject;

namespace Codemash.DeltaApi.Controllers
{
    public class NotificationController : ApiController
    {
        [Inject]
        public IClientRepository ClientRepository { get; set; }

        public ClientRegistrationResult Post(Client client)
        {
            try
            {
                if (!ClientRepository.IsClientRegistered(client.ChannelUri))
                {
                    ClientRepository.RegisterClient(client);
                }

                return new ClientRegistrationResult(client.ClientId);
            }
            catch (Exception ex)
            {
                // todo: add logging
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
