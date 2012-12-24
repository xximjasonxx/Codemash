using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Codemash.Phone.Core;
using Codemash.Phone.Data.Repository;
using Codemash.Phone.Shared.Common;
using Coding4Fun.Phone.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using Microsoft.Phone.Shell;
using Ninject;

namespace Codemash.Phone.Shared.Services.Impl
{
    public class CustomAppService : IAppService
    {
        private const string PushNotificationChannelName = "CodemashPushChannel";
        private string _clientTypeName = string.Empty;
        private HttpNotificationChannel _notificationChannel;
        private readonly PhoneApplicationFrame _rootFrame;

        [Inject]
        public INotificationRegistrationService RegistrationService { get; set; }

        [Inject]
        public ISettingsRepository SettingsRepository { get; set; }

        public CustomAppService(PhoneApplicationFrame rootFrame)
        {
            _rootFrame = rootFrame;
        }

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
                _notificationChannel.ChannelUriUpdated += NotificationChannel_ChannelUriUpdated;
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
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                                                          {
                                                              if (PushChannelInitializationFailure != null)
                                                                  PushChannelInitializationFailure(this, new EventArgs());
                                                          });
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
        public event EventHandler PushChannelInitializationFailure;

        private ProgressIndicator _progressIndicator;
        public void ShowProgressMessage(string message = "Waiting...")
        {
            _progressIndicator = new ProgressIndicator { IsIndeterminate = true };
            SystemTray.SetProgressIndicator((PhoneApplicationPage)_rootFrame.Content, _progressIndicator);
            _progressIndicator.IsVisible = true;
            _progressIndicator.Text = message;
        }

        public void HideProgressMessage()
        {
            if (_progressIndicator != null)
            {
                _progressIndicator.IsVisible = false;
                _progressIndicator = null;
            }
        }

        public void ShowToast(string title, string message, Action onTapHandler)
        {
            var toastPrompt = new ToastPrompt();
            toastPrompt.Title = title;
            toastPrompt.Message = message;
            toastPrompt.MillisecondsUntilHidden = 6000;
            toastPrompt.Background = new SolidColorBrush(Color.FromArgb(255, 251, 129, 2));
            toastPrompt.Tap += (sender, args) => onTapHandler.Invoke();

            toastPrompt.Show();
        }

        #endregion
    }
}
