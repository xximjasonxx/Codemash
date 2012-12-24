using System;
using Codemash.Phone.Shared.Common;
using Coding4Fun.Phone.Controls;

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
        /// Event indicating the initialization of the Push channel failed
        /// </summary>
        event EventHandler PushChannelInitializationFailure;

        /// <summary>
        /// Show a Progress message in the standard screen section
        /// </summary>
        /// <param name="message"></param>
        void ShowProgressMessage(string message);

        /// <summary>
        /// Hide any progress message currently displayed
        /// </summary>
        void HideProgressMessage();

        /// <summary>
        /// Show a Toast Prompt
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="onTapHandler"></param>
        void ShowToast(string title, string message, Action onTapHandler);
    }
}
