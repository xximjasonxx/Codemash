using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Ninject;

namespace Codemash.DeltaApi.Controllers
{
    public class NotificationController : ApiController
    {
        [Inject]
        public IClientRepository ClientRepository { get; set; }

        public ActionResult Post(Client client)
        {
            if (!ClientRepository.IsClientRegistered(client.ChannelUri))
            {
                ClientRepository.RegisterClient(client);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
