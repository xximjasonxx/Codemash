namespace Codemash.Notification.Factory.Impl
{
    public class NotificationFactory : INotificationFactory
    {
        public NotificationData BuildNotification(string channelUri, int missingChangesetCount)
        {
            var notificationData = new NotificationData();
            notificationData.ChannelUri = channelUri;

            // assign properties which will be null and thus not added to the XML
            notificationData.BackTitle = null;
            notificationData.Count = null;
            notificationData.BackBackgroundImageUrl = null;

            // now assign the easy properties
            notificationData.BackContent = string.Format("{0} update{1} {2} available", missingChangesetCount,
                                                         missingChangesetCount == 1 ? string.Empty : "s",
                                                         missingChangesetCount == 1 ? "is" : "are");

            // check if a tile image for this difference already exists

            // return our notification data
            return notificationData;
        }
    }
}
