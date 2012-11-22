using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Notification
{
    public class NotificationData
    {
        public string BackContent { get; internal set; }
        public string FrontBackgroundImageUrl { get; internal set; }
        public string BackBackgroundImageUrl { get; internal set; }

        public string ChannelUri { get; internal set; }
    }
}
