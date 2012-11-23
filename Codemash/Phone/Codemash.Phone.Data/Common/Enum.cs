
namespace Codemash.Phone.Data.Common
{
    internal enum EntityState
    {
        New,
        Modified,
        Removed,
        Clean
    }

    public enum ActionType
    {
        Add,
        Modify,
        Delete
    }
}
