using Codemash.Api.Data;

namespace Codemash.Notification.Factory
{
    public interface INotificationFactory
    {
        /// <summary>
        /// Build up the data needed to send a notification
        /// </summary>
        /// <param name="channelUri">The channel Uri the notification will be sent to</param>
        /// <param name="clientType">the type of client we are creating the notification for</param>
        /// <param name="missingChangesetCount">The missing changeset count</param>
        /// <returns>Notification data object</returns>
        TileNotificationData BuildTileNotification(string channelUri, string clientType, int missingChangesetCount);

        /// <summary>
        /// Construct a TileNotificationData class instance which will clear any notifications present on the client
        /// </summary>
        /// <param name="channelUri">The channel URI identifying the client</param>
        /// <param name="clientType">The type of client represented</param>
        /// <returns></returns>
        TileNotificationData BuildTileClearNotification(string channelUri, string clientType);

        /// <summary>
        /// Construct a ToastNotificationData to alert the user that changes have taken place
        /// </summary>
        /// <param name="channelUri"></param>
        /// <param name="clientType"></param>
        /// <param name="changesetCount"></param>
        /// <returns></returns>
        ToastNotificationData BuildToastNotification(string channelUri, string clientType, int changesetCount);
    }
}
