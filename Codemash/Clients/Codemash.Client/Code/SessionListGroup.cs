using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Code
{
    public class SessionListGroup : IListItem
    {
        public string GroupTitle { get; set; }

        #region Implementation of IListItem

        public string Display { get { return GroupTitle; } }
        public ItemType ItemType { get { return ItemType.GroupHeading; } }

        #endregion
    }
}
