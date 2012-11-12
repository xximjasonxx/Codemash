namespace Codemash.Api.Data.Entities.Interfaces
{
    public interface IChange
    {
        long ChangeEntityId { set; }
        ChangeAction ActionType { set; }
        string Key { set; }
        string Value { set; }
    }
}
