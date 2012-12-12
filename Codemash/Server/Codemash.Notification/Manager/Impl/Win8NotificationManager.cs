using System;
using System.IO;
using System.Net;
using System.Xml;
using Codemash.Notification.Context;
using Ninject;

namespace Codemash.Notification.Manager.Impl
{
    public class Win8NotificationManager : NotificationManagerBase, INotificationManager
    {
        [Inject]
        public ISecurityContext SecurityContext { get; set; }

        private const string ToastNotificationType = "wns/toast";
        private const string TileNotificationType = "wns/tile";

        private const string ToastContentType = "text/xml";
        private const string TileContentType = "text/xml";

        public void SendTileNotification(string channelUri, int changesetCount)
        {
            var request = (HttpWebRequest) WebRequest.Create(channelUri);
            request.Method = "POST";
            request.Headers.Add("X-WNS-Type", TileNotificationType);
            request.ContentType = TileContentType;
            request.Headers.Add("Authorization", String.Format("Bearer {0}", SecurityContext.AccessToken));

            const string title = "Codemash 2.0.1.3";
            var message = string.Format("{0} changeset{1}", changesetCount, changesetCount == 1 ? string.Empty : "s");

            byte[] payload;
            using (var memStream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(memStream))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("tile");
                    writer.WriteStartElement("visual");

                    writer.WriteStartElement("binding");
                    writer.WriteAttributeString("template", string.Empty, "TileSquareBlock");
                    
                    writer.WriteStartElement("text");
                    writer.WriteAttributeString("id", "1");
                    writer.WriteValue(changesetCount);
                    writer.WriteEndElement();

                    writer.WriteStartElement("text");
                    writer.WriteAttributeString("id", "2");
                    writer.WriteValue("changset" + (changesetCount == 1 ? string.Empty : "s"));
                    writer.WriteEndElement();

                    writer.WriteEndElement();

                    writer.WriteStartElement("binding");
                    writer.WriteAttributeString("template", string.Empty, "TileWideText01");

                    writer.WriteStartElement("text");
                    writer.WriteAttributeString("id", "1");
                    writer.WriteValue(title);
                    writer.WriteEndElement();

                    writer.WriteStartElement("text");
                    writer.WriteAttributeString("id", "2");
                    writer.WriteValue(message);
                    writer.WriteEndElement();

                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                payload = memStream.ToArray();
            }

            SendNotification(request, payload);
        }

        public void SendToastNotification(string channelUri, int changesetCount)
        {
            var request = (HttpWebRequest) WebRequest.Create(channelUri);
            request.Method = "POST";
            request.Headers.Add("X-WNS-Type", ToastNotificationType);
            request.ContentType = ToastContentType;
            request.Headers.Add("Authorization", String.Format("Bearer {0}", SecurityContext.AccessToken));

            const string title = "Codemash 2.0.1.3 Changes";
            var message = string.Format("{0} update{1} {2} available", changesetCount,
                                changesetCount == 1 ? string.Empty : "s", changesetCount == 1 ? "is" : "are");

            byte[] payload;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("toast");
                    writer.WriteStartElement("visual");
                    writer.WriteStartElement("binding");
                    writer.WriteAttributeString("template", string.Empty, "ToastText02");

                    writer.WriteStartElement("text");
                    writer.WriteAttributeString("id", string.Empty, "1");
                    writer.WriteValue(title);
                    writer.WriteEndElement();

                    writer.WriteStartElement("text");
                    writer.WriteAttributeString("id", string.Empty, "2");
                    writer.WriteValue(message);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();

                    payload = memoryStream.ToArray();
                }
            }

            //SendNotification(request, payload);
        }
    }
}
