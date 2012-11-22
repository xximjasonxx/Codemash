using System;
using System.IO;
using System.Net;
using System.Xml;

namespace Codemash.Notification.Manager.Impl
{
    public class WinPhone7NotificationManager : INotificationManager
    {
        #region Implementation of INotificationManager

        /// <summary>
        /// Send a notification
        /// </summary>
        /// <param name="notification"></param>
        public void SendNotification(NotificationData notification)
        {
            var payload = GetPayload(notification.FrontBackgroundImageUrl, notification.BackContent,
                notification.BackBackgroundImageUrl);
            var request = (HttpWebRequest)WebRequest.Create(notification.ChannelUri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentLength = payload.Length;
            request.ContentType = "text/xml";

            request.Headers.Add("X-Message-ID", Guid.NewGuid().ToString());
            request.Headers.Add("X-WindowsPhone-Target", "token");
            request.Headers.Add("X-NotificatioNClass", ((int) BatchingInterval.ImmediateTile).ToString());

            // send the request
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(payload, 0, payload.Length);
            }

            request.GetResponse();
        }

        #endregion

        /// <summary>
        /// Return the Payload that will be sent as part of the push notification
        /// </summary>
        /// <returns></returns>
        public byte[] GetPayload(string backImageUrl, string backContent, string backBackImageUrl)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("wp", "Notification", "WPNotification");
                    writer.WriteStartElement("wp", "Tile", "WPNotification");

                    writer.WriteStartElement("wp", "BackgroundImage", "WPNotification");
                    writer.WriteValue(backImageUrl);
                    writer.WriteEndElement();

                    writer.WriteStartElement("wp", "BackContent", "WPNotification");
                    writer.WriteValue(backContent);
                    writer.WriteEndElement();

                    writer.WriteStartElement("wp", "BackBackgroundImage", "WPNotification");
                    writer.WriteValue(backBackImageUrl);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();

                    return memoryStream.ToArray();
                }
            }
        }
    }
}
