
using System;
using System.Threading.Tasks;
using Codemash.Client.Data.Repository;
using Windows.Networking.PushNotifications;

namespace Codemash.Client.Common.Services.Impl
{
    public class CodemashApplicationService : IAppService
    {
        public ISettingsRepository SettingsRepository { get; private set; }

        public INotificationRegistrationService RegistrationService { get; private set; }

        public CodemashApplicationService(ISettingsRepository settingsRepository, INotificationRegistrationService registrationService)
        {
            SettingsRepository = settingsRepository;
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
                if (string.IsNullOrEmpty(SettingsRepository.RegisteredChannelUri) ||
                    string.Compare(channel.Uri, SettingsRepository.RegisteredChannelUri, StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    // register the URI with the service
                    var result = await RegistrationService.Register(channel.Uri);
                    if (!result.IsSuccess) return false;

                    // assign the URI to the local AppSettings
                    SettingsRepository.RegisteredChannelUri = channel.Uri;
                    if (result.ClientId.HasValue)
                        SettingsRepository.RegisteredClientId = result.ClientId.Value;
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
