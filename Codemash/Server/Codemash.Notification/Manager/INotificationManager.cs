using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Notification.Manager
{
    public interface INotificationManager
    {
        /// <summary>
        /// Send a Tile notification
        /// </summary>
        /// <param name="notification"></param>
        void SendTileNotification(TileNotificationData notification);

        /// <summary>
        /// Send a Toast notification
        /// </summary>
        /// <param name="notification"></param>
        void SendToastNotification(ToastNotificationData notification);
    }
}
