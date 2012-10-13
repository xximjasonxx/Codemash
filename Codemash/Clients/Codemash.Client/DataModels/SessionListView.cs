using Codemash.Client.Code;

namespace Codemash.Client.DataModels
{
    public class SessionListView : IListItem
    {
        public string SessionTitle { get; set; }
        public int SessionId { get; set; }

        #region Implementation of IListItem

        public string Display { get { return SessionTitle; } }
        public int Id { get { return SessionId; } }
        public ItemType ItemType { get { return ItemType.ListItem; } }

        #endregion
    }
}
