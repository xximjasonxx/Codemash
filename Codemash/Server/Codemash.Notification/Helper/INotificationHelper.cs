using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Notification.Helper
{
    public interface INotificationHelper
    {
        /// <summary>
        /// Get the ImageURL for the changeset count provided
        /// </summary>
        /// <param name="changesetCount"></param>
        /// <returns></returns>
        string GetImageUrlPathForCount(int changesetCount);
    }
}
