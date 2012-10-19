using Codemash.Phone7.App.Common;

namespace Codemash.Phone7.App.DataModels
{
    public interface IListItem
    {
        string Display { get; }
        SessionViewItemType ItemType { get; }
    }
}
