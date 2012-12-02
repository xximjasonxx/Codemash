using System;
using System.Linq;
using System.Windows.Media;
using Codemash.Phone.Data;
using Microsoft.Phone.Shell;

namespace Codemash.Phone8.App.Core
{
    public class Phone8TileService : ITileService
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
                IconicTileData tileData = new IconicTileData()
                                              {
                                                  Count = 0,
                                                  WideContent1 = string.Empty,
                                                  WideContent2 = string.Empty,
                                                  WideContent3 = string.Empty
                                              };

                tile.Update(tileData);
            }
        }

        #endregion
    }
}
