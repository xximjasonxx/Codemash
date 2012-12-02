using System;
using System.Collections.ObjectModel;
using System.Windows;
using Codemash.Phone.Core;
using Codemash.Phone.Data.Repository;
using Codemash.Phone.Shared.Common;
using Microsoft.Phone.Notification;
using Ninject;

namespace Codemash.Phone.Shared.Services.Impl
{
    public class CustomAppService : IAppService
    {
        private const string PushNotificationChannelName = "CodemashPushChannel";
        private string _clientTypeName = string.Empty;
        private HttpNotificationChannel _notificationChannel;

        [Inject]
        public INotificationRegistrationService RegistrationService { get; set; }

        [Inject]
        public ISettingsRepository SettingsRepository { get; set; }

        #region Implementation of IAppService

        /// <summary>
        /// Called to setup the push notification channel
        /// </summary>
        /// <param name="clientType"> </param>
        public void InitializePushChannel(PhoneClientType clientType)
        {
            _clientTypeName = clientType.ToString();
            _notificationChannel = HttpNotificationChannel.Find(PushNotificationChannelName);
            if (_notificationChannel == null)
            {
                _notificationChannel = new HttpNotificationChannel(PushNotificationChannelName);
                _notificationChannel.ChannelUriUpdated += NotificationChannel_ChannelUriUpdated;

                // open the channel
                _notificationChannel.Open();
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
            SettingsRepository.PushChannelUri = _notificationChannel.ChannelUri.ToString();
            if (!_notificationChannel.IsShellTileBound)
                _notificationChannel.BindToShellTile(new Collection<Uri>()
                                                         {
                                                             new Uri(Config.DeltaApiDomain, UriKind.RelativeOrAbsolute)
                                                         });

            if (!_notificationChannel.IsShellToastBound)
                _notificationChannel.BindToShellToast();

            if (PushChannelInitialized != null)
                PushChannelInitialized(this, new EventArgs());
        }

        public event EventHandler PushChannelInitialized;

        #endregion
    }
}
