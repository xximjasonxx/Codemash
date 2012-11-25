
using Ninject;

namespace Codemash.Notification.Factory.Impl
{
    public class NotificationFactory : INotificationFactory
    {
        [Inject]
        public INotificationHelperResolver NotificationHelperResolver { get; set; }

        public TileNotificationData BuildTileNotification(string channelUri, string clientType, int missingChangesetCount)
        {
            var notificationData = new TileNotificationData();
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
        public TileNotificationData BuildTileClearNotification(string channelUri, string clientType)
        {
            var helper = NotificationHelperResolver.Resolve(clientType);

            return new TileNotificationData
                       {
                           Count = 0,
                           ChannelUri = channelUri,
                           BackBackgroundImageUrl = null,
                           BackContent = string.Empty,
                           FrontBackgroundImageUrl = helper.GetNoneImageUrl()
                       };
        }

        /// <summary>
        /// Construct a ToastNotificationData to alert the user that changes have taken place
        /// </summary>
        /// <param name="channelUri"></param>
        /// <param name="clientType"></param>
        /// <param name="changesetCount"></param>
        /// <returns></returns>
        public ToastNotificationData BuildToastNotification(string channelUri, string clientType, int changesetCount)
        {
            var notificationData = new ToastNotificationData();
            notificationData.ChannelUri = channelUri;
            notificationData.Title = "Codemash";
            notificationData.Message = string.Format("{0} update{1} {2} available", changesetCount,
                                                         changesetCount == 1 ? string.Empty : "s",
                                                         changesetCount == 1 ? "is" : "are");

            return notificationData;
        }
    }
}
