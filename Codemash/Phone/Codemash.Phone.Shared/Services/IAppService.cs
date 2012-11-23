using System;
using Codemash.Phone.Shared.Common;

namespace Codemash.Phone.Shared.Services
{
    public interface IAppService
    {
        /// <summary>
        /// Called to setup the push notification channel
        /// </summary>
        /// <param name="clientType"> </param>
        void InitializePushChannel(PhoneClientType clientType);

        /// <summary>
        /// Event indicating that the Push Channel has been initialized
        /// </summary>
        event EventHandler PushChannelInitialized;
    }
}
