using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Classes
{
    public class SessionGroup : IListItem
    {
        public string Title { get; set; }

        #region Implementation of IListItem

        public string Display { get { return Title; } }
        public ItemType ItemType { get { return ItemType.GroupHeading;} }

        #endregion
    }
}
