using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Notification.Manager.Impl
{
    public class WinPhone8NotificationManager : INotificationManager
    {
        #region Implementation of INotificationManager

        /// <summary>
        /// Send a Tile notification
        /// </summary>
        /// <param name="channelUri"> </param>
        /// <param name="changeCount"> </param>
        public void SendTileNotification(string channelUri, int changeCount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send a notification to the client instructing the tile to revert to its pre notification state
        /// </summary>
        /// <param name="channelUri"></param>
        public void SendClearTileNotification(string channelUri)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send a Toast notification
        /// </summary>
        /// <param name="channelUri"> </param>
        /// <param name="changesetCount"> </param>
        public void SendToastNotification(string channelUri, int changesetCount)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
