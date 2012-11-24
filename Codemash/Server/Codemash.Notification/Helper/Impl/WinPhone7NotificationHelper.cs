using Codemash.Server.Core;

namespace Codemash.Notification.Helper.Impl
{
    public class WinPhone7NotificationHelper : INotificationHelper
    {
        #region Implementation of INotificationHelper
        /// <summary>
        /// Get the ImageURL for the changeset count provided
        /// </summary>
        /// <param name="changesetCount"></param>
        /// <returns></returns>
        public string GetImageUrlPathForCount(int changesetCount)
        {
            return string.Format("{0}/Handlers/Phone7Notification.ashx?count={1}", Config.DeltaApiDomain, changesetCount);
        }

        /// <summary>
        /// Returns the image used as the background for the back of the tile
        /// </summary>
        /// <returns></returns>
        public string GetBackImageUrlPath()
        {
            return string.Format("{0}/Handlers/Images/wp7/phone7_back.png", Config.DeltaApiDomain);
        }

        /// <summary>
        /// Returns the imaged used as the back for no notifications
        /// </summary>
        /// <returns></returns>
        public string GetNoneImageUrl()
        {
            return string.Format("{0}/Handlers/Images/wp7/phone7_none.png", Config.DeltaApiDomain);
        }

        #endregion
    }
}
