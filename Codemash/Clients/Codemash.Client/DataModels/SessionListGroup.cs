using Codemash.Client.Code;

namespace Codemash.Client.DataModels
{
    public class SessionListGroup : IListItem
    {
        public string GroupTitle { get; set; }

        #region Implementation of IListItem

        public string Display { get { return GroupTitle; } }
        public int Id { get { return 0; } }
        public ItemType ItemType { get { return ItemType.GroupHeading; } }

        #endregion
    }
}
