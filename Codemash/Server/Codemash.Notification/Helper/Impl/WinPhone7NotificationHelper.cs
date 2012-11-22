using System.Drawing;
using System.IO;
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
            return string.Format("{0}/Phone7Notification.ashx?count={1}", Config.DeltaApiDomain, changesetCount);
        }

        #endregion
    }
}
