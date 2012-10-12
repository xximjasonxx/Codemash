namespace Codemash.Client.Code
{
    public interface IListItem
    {
        string Display { get; }
        ItemType ItemType { get; }
    }
}
