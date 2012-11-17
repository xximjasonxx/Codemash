using System;
using System.IO;
using System.Net;
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
            var payload = GetPayload(title, subtitle);
            var request = (HttpWebRequest)WebRequest.Create(channelUri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentLength = payload.Length;
            request.ContentType = "text/xml";

            request.Headers.Add("X-Message-ID", Guid.NewGuid().ToString());
            request.Headers.Add("X-WindowsPhone-Target", "toast");
            request.Headers.Add("X-NotificationClass", ((int) BatchingInterval.ImemdiateToast).ToString());

            // send the request
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(payload, 0, payload.Length);
            }

            request.GetResponse();
        }

        #endregion

        private byte[] GetPayload(string title, string subtitle)
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

                return memoryStream.ToArray();
            }
        }
    }
}
