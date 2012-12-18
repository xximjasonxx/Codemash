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

        /// <summary>
        /// Show a Progress message in the standard screen section
        /// </summary>
        /// <param name="message"></param>
        void ShowProgressMessage(string message);

        /// <summary>
        /// Hide any progress message currently displayed
        /// </summary>
        void HideProgressMessage();
    }
}
