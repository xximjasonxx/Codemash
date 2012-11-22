using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Notification.Manager
{
    public interface INotificationManager
    {
        /// <summary>
        /// Send a notification
        /// </summary>
        /// <param name="notification"></param>
        void SendNotification(NotificationData notification);
    }
}
