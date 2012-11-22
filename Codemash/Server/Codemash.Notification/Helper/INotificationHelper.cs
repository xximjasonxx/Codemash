using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Notification.Helper
{
    public interface INotificationHelper
    {
        /// <summary>
        /// Get the ImageURL for the changeset count provided (tile front background)
        /// </summary>
        /// <param name="changesetCount"></param>
        /// <returns></returns>
        string GetImageUrlPathForCount(int changesetCount);

        /// <summary>
        /// Returns the image used as the background for the back of the tile
        /// </summary>
        /// <returns></returns>
        string GetBackImageUrlPath();
    }
}
