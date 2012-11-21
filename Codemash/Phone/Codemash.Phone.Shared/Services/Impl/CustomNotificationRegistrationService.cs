using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using RestSharp;

namespace Codemash.Phone.Shared.Services.Impl
{
    public class CustomNotificationRegistrationService : INotificationRegistrationService
    {
        #region Implementation of INotificationRegistrationService

        /// <summary>
        /// Register the Channel URI and Client Name with the Nofification tracking service
        /// </summary>
        /// <param name="channelUri"></param>
        /// <param name="clientTypeName"></param>
        public void Register(string channelUri, string clientTypeName)
        {
            var client = new RestClient("http://192.168.1.101/DeltaApi/api/");
            var request = new RestRequest("Notification", Method.POST);
            request.AddParameter("ChannelUri", channelUri);
            request.AddParameter("ClientType", clientTypeName);

            client.ExecuteAsync(request, (resp, handle) =>
                                             {
                                                 if (resp.StatusCode == HttpStatusCode.OK)
                                                 {
                                                     if (RegistrationCompleted != null)
                                                         RegistrationCompleted(this, new EventArgs());
                                                 }
                                                 else
                                                 {
                                                     if (RegistrationFailed != null)
                                                         RegistrationFailed(this, new EventArgs());
                                                 }
                                             });
        }

        public event EventHandler RegistrationCompleted;
        public event EventHandler RegistrationFailed;

        #endregion
    }
}
