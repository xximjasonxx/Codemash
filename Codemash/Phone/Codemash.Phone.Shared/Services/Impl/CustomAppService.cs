using System;
using System.Windows;
using Codemash.Phone.Shared.Common;
using Microsoft.Phone.Notification;
using Ninject;

namespace Codemash.Phone.Shared.Services.Impl
{
    public class CustomAppService : IAppService
    {
        private const string PushNotificationChannelName = "CodemashPushChannel";
        private string _clientTypeName = string.Empty;

        [Inject]
        public INotificationRegistrationService RegistrationService { get; set; }

        #region Implementation of IAppService

        /// <summary>
        /// Called to setup the push notification channel
        /// </summary>
        /// <param name="clientType"> </param>
        public void InitializePushChannel(PhoneClientType clientType)
        {
            _clientTypeName = clientType.ToString();
            var notificationChannel = HttpNotificationChannel.Find(PushNotificationChannelName);
            if (notificationChannel == null)
            {
                notificationChannel = new HttpNotificationChannel(PushNotificationChannelName);
                notificationChannel.ChannelUriUpdated += NotificationChannel_ChannelUriUpdated;

                // open the channel
                notificationChannel.Open();
            }
            else
            {
                if (PushChannelInitialized != null)
                    PushChannelInitialized(this, new EventArgs());
            }
        }

        void NotificationChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            RegistrationService.RegistrationCompleted += RegistrationService_RegistrationCompleted;
            RegistrationService.RegistrationFailed += RegistrationService_RegistrationFailed;
            RegistrationService.Register(e.ChannelUri.ToString(), _clientTypeName);
        }

        void RegistrationService_RegistrationFailed(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("Push Notification Registration Failed"));
        }

        void RegistrationService_RegistrationCompleted(object sender, EventArgs e)
        {
            if (PushChannelInitialized != null)
                PushChannelInitialized(this, new EventArgs());
        }

        public event EventHandler PushChannelInitialized;

        #endregion
    }
}
