namespace Codemash.Client.Code
{
    public interface IListItem
    {
        string Display { get; }
        int Id { get; }
        ItemType ItemType { get; }
    }
}
