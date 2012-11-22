using Codemash.Api.Data;
using Ninject;

namespace Codemash.Notification.Factory.Impl
{
    public class NotificationFactory : INotificationFactory
    {
        [Inject]
        public INotificationHelperResolver NotificationHelperResolver { get; set; }

        public NotificationData BuildNotification(string channelUri, string clientType, int missingChangesetCount)
        {
            var notificationData = new NotificationData();
            notificationData.ChannelUri = channelUri;

            notificationData.BackContent = string.Format("{0} update{1} {2} available", missingChangesetCount,
                                                         missingChangesetCount == 1 ? string.Empty : "s",
                                                         missingChangesetCount == 1 ? "is" : "are");

            var helper = NotificationHelperResolver.Resolve(clientType);
            notificationData.FrontBackgroundImageUrl = helper.GetImageUrlPathForCount(missingChangesetCount);

            // return our notification data
            return notificationData;
        }
    }
}
