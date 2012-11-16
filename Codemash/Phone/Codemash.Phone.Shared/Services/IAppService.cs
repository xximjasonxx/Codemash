using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Phone.Notification;

namespace Codemash.Phone.Shared.Services
{
    public interface IAppService
    {
        /// <summary>
        /// Called to setup the push notification channel
        /// </summary>
        void InitializePushChannel();

        /// <summary>
        /// Event indicating that the Push Channel has been initialized
        /// </summary>
        event EventHandler PushChannelInitialized;
    }
}
