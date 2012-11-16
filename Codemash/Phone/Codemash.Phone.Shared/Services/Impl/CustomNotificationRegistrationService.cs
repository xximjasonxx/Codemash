using System;
using System.Collections.Generic;
using System.Linq;
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
            
        }

        public event EventHandler RegistrationCompleted;

        #endregion
    }
}
