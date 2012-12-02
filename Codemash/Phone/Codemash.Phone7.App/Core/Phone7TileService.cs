using System;
using System.Linq;
using Codemash.Phone.Data;
using Microsoft.Phone.Shell;

namespace Codemash.Phone7.App.Core
{
    public class Phone7TileService : ITileService
    {
        #region Implementation of ITileService

        /// <summary>
        /// Clear the notification for a primary tile pinned to the start screen
        /// </summary>
        public void ClearTileNotification()
        {
            var tile = ShellTile.ActiveTiles.FirstOrDefault();
            if (tile != null)
            {
                var data = new StandardTileData()
                               {
                                   BackBackgroundImage = new Uri("/ThisDoesntExist.png", UriKind.RelativeOrAbsolute),
                                   BackContent = string.Empty,
                                   BackTitle = string.Empty,
                                   BackgroundImage = new Uri("/ApplicationTile.png", UriKind.RelativeOrAbsolute),
                                   Count = 0
                               };

                tile.Update(data);
            }
        }

        #endregion
    }
}
