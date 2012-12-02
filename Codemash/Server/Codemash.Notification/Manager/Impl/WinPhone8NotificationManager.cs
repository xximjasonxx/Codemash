using System;
using System.IO;
using System.Net;
using System.Xml;

namespace Codemash.Notification.Manager.Impl
{
    public class WinPhone8NotificationManager : NotificationManagerBase, INotificationManager
    {
        #region Implementation of INotificationManager

        /// <summary>
        /// Send a Tile notification
        /// </summary>
        /// <param name="channelUri"> </param>
        /// <param name="changeCount"> </param>
        public void SendTileNotification(string channelUri, int changeCount)
        {
            var payload = GetTilePayload(changeCount);
            var request = (HttpWebRequest)WebRequest.Create(channelUri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentLength = payload.Length;
            request.ContentType = "text/xml";

            request.Headers.Add("X-Message-ID", Guid.NewGuid().ToString());
            request.Headers.Add("X-WindowsPhone-Target", "token");
            request.Headers.Add("X-NotificationClass", ((int)BatchingInterval.ImmediateTile).ToString());

            // send the request
            SendNotification(request, payload);
        }

        /// <summary>
        /// Send a notification to the client instructing the tile to revert to its pre notification state
        /// </summary>
        /// <param name="channelUri"></param>
        public void SendClearTileNotification(string channelUri)
        {
            var payload = GetClearTilePayload();
            var request = (HttpWebRequest)WebRequest.Create(channelUri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentLength = payload.Length;
            request.ContentType = "text/xml";

            request.Headers.Add("X-Message-ID", Guid.NewGuid().ToString());
            request.Headers.Add("X-WindowsPhone-Target", "token");
            request.Headers.Add("X-NotificationClass", ((int)BatchingInterval.ImmediateTile).ToString());

            // send the request
            SendNotification(request, payload);
        }

        /// <summary>
        /// Send a Toast notification
        /// </summary>
        /// <param name="channelUri"> </param>
        /// <param name="changesetCount"> </param>
        public void SendToastNotification(string channelUri, int changesetCount)
        {
            var payload = GetToastPayload(changesetCount);
            var request = (HttpWebRequest)WebRequest.Create(channelUri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentLength = payload.Length;
            request.ContentType = "text/xml";

            request.Headers.Add("X-Message-ID", Guid.NewGuid().ToString());
            request.Headers.Add("X-WindowsPhone-Target", "toast");
            request.Headers.Add("X-NotificationClass", ((int)BatchingInterval.ImemdiateToast).ToString());

            SendNotification(request, payload);
        }

        #endregion

        private byte[] GetTilePayload(int changesetCount)
        {
            const string wideContent = "Codemash 2013 Update";
            var wideContent2 = string.Format("{0} update{1} {2} available", changesetCount,
                                            changesetCount == 1 ? string.Empty : "s", changesetCount == 1 ? "is" : "are");

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("wp", "Notification", "WPNotification");
                    writer.WriteStartElement("wp", "Tile", "WPNotification");
                    writer.WriteAttributeString("Template", "IconicTile");

                    writer.WriteStartElement("wp", "WideContent1", "WPNotification");
                    writer.WriteValue(wideContent);
                    writer.WriteEndElement();

                    writer.WriteStartElement("wp", "WideContent2", "WPNotification");
                    writer.WriteValue(wideContent2);
                    writer.WriteEndElement();

                    writer.WriteStartElement("wp", "Count", "WPNotification");
                    writer.WriteValue(changesetCount);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                }

                return memoryStream.ToArray();
            }
        }

        private byte[] GetClearTilePayload()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("wp", "Notification", "WPNotification");
                    writer.WriteStartElement("wp", "Tile", "WPNotification");
                    writer.WriteAttributeString("Template", "IconicTile");

                    writer.WriteStartElement("wp", "WideContent1", "WPNotification");
                    writer.WriteStartAttribute("Action", "Clear");
                    writer.WriteEndElement();

                    writer.WriteStartElement("wp", "WideContent2", "WPNotification");
                    writer.WriteStartAttribute("Action", "Clear");
                    writer.WriteEndElement();

                    writer.WriteStartElement("wp", "Count", "WPNotification");
                    writer.WriteStartAttribute("Action", "Clear");
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                }

                return memoryStream.ToArray();
            }
        }

        private byte[] GetToastPayload(int changesetCount)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("wp", "Notification", "WPNotification");
                    writer.WriteStartElement("wp", "Toast", "WPNotification");

                    writer.WriteStartElement("wp", "Text1", "WPNotification");
                    writer.WriteValue("Codemash");
                    writer.WriteEndElement();

                    string message = string.Format("{0} update{1} {2} available", changesetCount,
                                                   changesetCount == 1 ? string.Empty : "s",
                                                   changesetCount == 1 ? "is" : "are");
                    writer.WriteStartElement("wp", "Text2", "WPNotification");
                    writer.WriteValue(message);
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
