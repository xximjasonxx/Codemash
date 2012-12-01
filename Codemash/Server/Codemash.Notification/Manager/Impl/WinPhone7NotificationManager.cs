using System;
using System.IO;
using System.Net;
using System.Xml;
using Codemash.Server.Core;

namespace Codemash.Notification.Manager.Impl
{
    public class WinPhone7NotificationManager : INotificationManager
    {
        #region Implementation of INotificationManager

        /// <summary>
        /// Send a notification
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
            request.Headers.Add("X-NotificationClass", ((int) BatchingInterval.ImmediateTile).ToString());

            // send the request
            SendNotification(request, payload);
        }

        /// <summary>
        /// Send a notification to the client instructing the tile to revert to its pre notification state
        /// </summary>
        /// <param name="channelUri"></param>
        public void SendClearTileNotification(string channelUri)
        {
            var payload = GetTileClearPayload();
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
            request.Headers.Add("X-NotificationClass", ((int) BatchingInterval.ImemdiateToast).ToString());

            SendNotification(request, payload);
        }

        #endregion

        /// <summary>
        /// Return the Payload that will be sent as part of the push notification
        /// </summary>
        /// <returns></returns>
        private byte[] GetTilePayload(int changesetCount)
        {
            var backContent = string.Format("{0} update{1} {2} available", changesetCount,
                                            changesetCount == 1 ? string.Empty : "s", changesetCount == 1 ? "is" : "are");
            var frontBackImage = string.Format("{0}/Handlers/Phone7Notification.ashx?count={1}", Config.DeltaApiDomain,
                                               changesetCount);
            var backBackImage = string.Format("{0}/Handlers/Images/wp7/phone7_back.png", Config.DeltaApiDomain);


            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("wp", "Notification", "WPNotification");
                    writer.WriteStartElement("wp", "Tile", "WPNotification");

                    writer.WriteStartElement("wp", "BackgroundImage", "WPNotification");
                    writer.WriteValue(frontBackImage);
                    writer.WriteEndElement();

                    writer.WriteStartElement("wp", "BackContent", "WPNotification");
                    writer.WriteValue(backContent);
                    writer.WriteEndElement();

                    writer.WriteStartElement("wp", "BackBackgroundImage", "WPNotification");
                    writer.WriteValue(backBackImage);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();

                    return memoryStream.ToArray();
                }
            }
        }

        private byte[] GetTileClearPayload()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("wp", "Notification", "WPNotification");
                    writer.WriteStartElement("wp", "Tile", "WPNotification");

                    writer.WriteStartElement("wp", "BackgroundImage", "WPNotification");
                    writer.WriteValue(string.Format("{0}/Handlers/Images/wp7/phone7_none.png", Config.DeltaApiDomain));
                    writer.WriteEndElement();

                    writer.WriteStartElement("wp", "BackContent", "WPNotification");
                    writer.WriteAttributeString("Action", "Clear");
                    writer.WriteEndElement();

                    writer.WriteStartElement("wp", "BackBackgroundImage", "WPNotification");
                    writer.WriteAttributeString("Action", "Clear");
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();

                    return memoryStream.ToArray();
                }
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
