
using System;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;

namespace Codemash.Client.Common.Services.Impl
{
    public class CodemashApplicationService : IAppService
    {
        public ISettingsService SettingsService { get; private set; }

        public INotificationRegistrationService RegistrationService { get; private set; }

        public CodemashApplicationService(ISettingsService settingsService, INotificationRegistrationService registrationService)
        {
            SettingsService = settingsService;
            RegistrationService = registrationService;
        }

        #region Implementation of IAppService

        /// <summary>
        /// Initialize the Push channel to allow the device to receive notifications
        /// </summary>
        public async Task<bool> InitalizePushChannel()
        {
            try
            {
                var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                if (string.IsNullOrEmpty(SettingsService.RegisteredChannelUri) ||
                    string.Compare(channel.Uri, SettingsService.RegisteredChannelUri, StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    // register the URI with the service
                    var result = await RegistrationService.Register(channel.Uri);
                    if (!result.IsSuccess) return false;

                    // assign the URI to the local AppSettings
                    SettingsService.RegisteredChannelUri = channel.Uri;
                }

                // prepare to handle incoming push notifications
                channel.PushNotificationReceived += channel_PushNotificationReceived;
                return true;
            }
            catch
            {
                // something went wrong
                return false;
            }
        }

        void channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
