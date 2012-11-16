using System.IO;
using System.Xml;

namespace Codemash.Poller.Notification.Impl.Manager
{
    public class WinPhone8NotificationManager : NotificationManagerBase, INotificationManager
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
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {

                    writer.WriteStartDocument();
                    writer.WriteStartElement("wp", "Notification", "WPNotification");
                    writer.WriteStartElement("wp", "Toast", "WPNotification");

                    writer.WriteStartElement("wp", "Text1", "WPNotification");
                    writer.WriteValue(title);
                    writer.WriteEndElement();

                    writer.WriteStartElement("wp", "Text2", "WPNotification");
                    writer.WriteValue(subtitle);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                }

                byte[] payload = memoryStream.ToArray();
            }
        }

        #endregion
    }
}
