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
        public void SendTileNotification(TileNotificationData notification)
        {
            var payload = GetPayload(notification);
            var request = (HttpWebRequest)WebRequest.Create(notification.ChannelUri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentLength = payload.Length;
            request.ContentType = "text/xml";

            request.Headers.Add("X-Message-ID", Guid.NewGuid().ToString());
            request.Headers.Add("X-WindowsPhone-Target", "token");
            request.Headers.Add("X-NotificationClass", ((int) BatchingInterval.ImmediateTile).ToString());

            // send the request
            SendNotification(request, payload);
        }

        /// <summary>
        /// Send a Toast notification
        /// </summary>
        /// <param name="data"></param>
        public void SendToastNotification(ToastNotificationData data)
        {
            var payload = GetPayload(data);
            var request = (HttpWebRequest)WebRequest.Create(data.ChannelUri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentLength = payload.Length;
            request.ContentType = "text/xml";

            request.Headers.Add("X-Message-ID", Guid.NewGuid().ToString());
            request.Headers.Add("X-WindowsPhone-Target", "toast");
            request.Headers.Add("X-NotificationClass", ((int) BatchingInterval.ImemdiateToast).ToString());

            SendNotification(request, payload);
        }

        #endregion

        /// <summary>
        /// Return the Payload that will be sent as part of the push notification
        /// </summary>
        /// <returns></returns>
        private byte[] GetPayload(TileNotificationData data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("wp", "Notification", "WPNotification");
                    writer.WriteStartElement("wp", "Tile", "WPNotification");

                    if (data.FrontBackgroundImageUrl != null)
                    {
                        writer.WriteStartElement("wp", "BackgroundImage", "WPNotification");
                        writer.WriteValue(data.FrontBackgroundImageUrl);
                        writer.WriteEndElement();
                    }

                    if (data.Count.HasValue)
                    {
                        writer.WriteStartElement("wp", "Count", "WPNotification");
                        writer.WriteValue(data.Count.Value);
                        writer.WriteEndElement();
                    }

                    if (data.BackContent != null)
                    {
                        writer.WriteStartElement("wp", "BackContent", "WPNotification");
                        writer.WriteValue(data.BackContent);
                        writer.WriteEndElement();
                    }

                    if (data.BackBackgroundImageUrl != null)
                    {
                        writer.WriteStartElement("wp", "BackBackgroundImage", "WPNotification");
                        writer.WriteValue(data.BackBackgroundImageUrl);
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();

                    return memoryStream.ToArray();
                }
            }
        }

        private byte[] GetPayload(ToastNotificationData data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {

                    writer.WriteStartDocument();
                    writer.WriteStartElement("wp", "Notification", "WPNotification");
                    writer.WriteStartElement("wp", "Toast", "WPNotification");

                    if (data.Title != null)
                    {
                        writer.WriteStartElement("wp", "Text1", "WPNotification");
                        writer.WriteValue(data.Title);
                        writer.WriteEndElement();
                    }

                    if (data.Message != null)
                    {
                        writer.WriteStartElement("wp", "Text2", "WPNotification");
                        writer.WriteValue(data.Message);
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                }

                return memoryStream.ToArray();
            }
        }

        private void SendNotification(WebRequest request, byte[] payload)
        {
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(payload, 0, payload.Length);
            }

            try
            {
                request.GetResponse();
            }
            catch (WebException ex) { /* this happens if certain channels are no longer active */ }
        }
    }
}
