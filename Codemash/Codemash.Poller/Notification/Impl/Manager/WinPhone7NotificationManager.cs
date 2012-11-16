namespace Codemash.Poller.Notification.Impl.Manager
{
    public class WinPhone7NotificationManager : NotificationManagerBase, INotificationManager
    {
        #region Implementation of INotificationManager

        /// <summary>
        /// Send a Toast Notification
        /// </summary>
        /// <param name="channelUri">The channel URI which identifies the push channel to be used</param>
        /// <param name="title">The title (Text1) of the Toast notification</param>
        /// <param name="subtitle">The subtitle (Text2) of the Toast notification</param>
        public void SendToast(string channelUri, string title, string subtitle)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
