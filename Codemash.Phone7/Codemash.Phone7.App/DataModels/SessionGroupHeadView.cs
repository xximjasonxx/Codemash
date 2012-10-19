using Codemash.Phone7.App.Common;

namespace Codemash.Phone7.App.DataModels
{
    public class SessionGroupHeadView : IListItem
    {
        public string Title { get; set; }
        public string Display { get { return Title; } }
        public SessionViewItemType ItemType { get { return SessionViewItemType.GroupHead; } }
    }
}
