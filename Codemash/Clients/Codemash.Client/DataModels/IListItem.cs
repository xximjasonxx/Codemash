using Codemash.Client.Code;

namespace Codemash.Client.DataModels
{
    public interface IListItem
    {
        string Display { get; }
        int Id { get; }
        ItemType ItemType { get; }
    }
}
