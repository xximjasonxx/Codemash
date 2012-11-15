using Microsoft.Phone.Notification;

namespace Codemash.Phone.Shared.Services.Impl
{
    public class CustomAppService : IAppService
    {
        private const string PushNotificationChannelName = "CodemashPushChannel";

        public CustomAppService()
        {
            NotificationChannel = HttpNotificationChannel.Find(PushNotificationChannelName);
            if (NotificationChannel == null)
            {
                NotificationChannel = new HttpNotificationChannel(PushNotificationChannelName);
                NotificationChannel.Open();
                NotificationChannel.BindToShellToast();

                // save the channel URI
                string channelUri = NotificationChannel.ChannelUri.ToString();
            }
        }

        #region Implementation of IAppService

        public HttpNotificationChannel NotificationChannel { get; private set; }

        #endregion
    }
}
