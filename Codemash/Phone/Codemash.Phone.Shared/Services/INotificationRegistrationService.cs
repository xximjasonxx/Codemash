using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Phone.Shared.Services
{
    public interface INotificationRegistrationService
    {
        /// <summary>
        /// Register the Channel URI and Client Name with the Nofification tracking service
        /// </summary>
        /// <param name="channelUri"></param>
        /// <param name="clientTypeName"></param>
        void Register(string channelUri, string clientTypeName);

        /// <summary>
        /// Event indicating the client has successfully registered wit the NotificationTracking Service
        /// </summary>
        event EventHandler RegistrationCompleted;

        /// <summary>
        /// Event indicating that registration of the client for Push notifications has failed
        /// </summary>
        event EventHandler RegistrationFailed;
    }
}
