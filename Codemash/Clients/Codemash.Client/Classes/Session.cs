using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Classes
{
    public class Session : IListItem
    {
        public string Title { get; set; }
        public string SpeakerName { get; set; }
        public string Room { get; set; }
        public string Time { get; set; }
        public string Track { get; set; }
        public string Level { get { return "Intermediate"; } }
        public Speaker Speaker { get { return new Speaker(); } }

        #region Implementation of IListItem

        public string Display { get { return Title; } }
        public ItemType ItemType { get { return ItemType.ListItem; } }

        #endregion
    }
}
