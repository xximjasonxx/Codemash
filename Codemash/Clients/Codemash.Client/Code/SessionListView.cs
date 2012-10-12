using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Code
{
    public class SessionListView : IListItem
    {
        public string SessionTitle { get; set; }

        #region Implementation of IListItem

        public string Display { get { return SessionTitle; } }
        public ItemType ItemType { get { return ItemType.ListItem; } }

        #endregion
    }
}
