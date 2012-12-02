using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Codemash.Notification.Manager.Impl
{
    public abstract class NotificationManagerBase
    {
        protected void SendNotification(WebRequest request, byte[] payload)
        {
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(payload, 0, payload.Length);
            }

            try
            {
                request.GetResponse();
            }
            catch (WebException ex)
            {
                return;
                /* this happens if certain channels are no longer active */
            }
        }
    }
}
