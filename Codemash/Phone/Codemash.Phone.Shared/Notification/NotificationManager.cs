using System.Linq;
using Microsoft.Phone.Shell;

namespace Codemash.Phone.Shared.Notification
{
    public static class NotificationManager
    {
        public static void ResetTile()
        {
            StandardTileData tileData = new StandardTileData();
            tileData.Count = 0;

            var tile = ShellTile.ActiveTiles.FirstOrDefault();
            if (tile != null)
            {
                tile.Update(tileData);
            }
        }
    }
}
