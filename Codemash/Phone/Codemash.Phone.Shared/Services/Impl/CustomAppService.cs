using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Phone.Shared.Common;
using Microsoft.Phone.Notification;

namespace Codemash.Phone.Shared.Services.Impl
{
    public class CustomAppService : IAppService
    {
        private const string PushNotificationChannelName = "CodemashPushChannel";
        private const string ApplicationServiceName = "CodemashDeltaService";

        private HttpNotificationChannel _notificationChannel;
        private string _clientTypeName = string.Empty;

        public CustomAppService(PhoneClientType clientTypeName)
        {
            _clientTypeName = clientTypeName.ToString();
        }

        #region Implementation of IAppService

        /// <summary>
        /// Called to setup the push notification channel
        /// </summary>
        public void InitializePushChannel()
        {
            _notificationChannel = HttpNotificationChannel.Find(PushNotificationChannelName);
            if (_notificationChannel == null)
            {
                _notificationChannel = new HttpNotificationChannel(PushNotificationChannelName, ApplicationServiceName);
                _notificationChannel.ChannelUriUpdated += NotificationChannel_ChannelUriUpdated;
            }
        }

        void NotificationChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            
        }

        public event EventHandler PushChannelInitialized;

        #endregion
    }
}
