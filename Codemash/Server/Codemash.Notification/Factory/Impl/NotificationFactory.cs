
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
            notificationData.BackBackgroundImageUrl = helper.GetBackImageUrlPath();

            // return our notification data
            return notificationData;
        }

        /// <summary>
        /// Construct a NotificationData class instance which will clear any notifications present on the client
        /// </summary>
        /// <param name="channelUri">The channel URI identifying the client</param>
        /// <param name="clientType">The type of client represented</param>
        /// <returns></returns>
        public NotificationData BuildClearNotification(string channelUri, string clientType)
        {
            var helper = NotificationHelperResolver.Resolve(clientType);

            return new NotificationData
                       {
                           Count = 0,
                           ChannelUri = channelUri,
                           BackBackgroundImageUrl = null,
                           BackContent = string.Empty,
                           FrontBackgroundImageUrl = helper.GetNoneImageUrl()
                       };
        }
    }
}
