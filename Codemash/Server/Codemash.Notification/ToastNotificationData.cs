using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Notification
{
    public class ToastNotificationData
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public string ChannelUri { get; set; }
    }
}
