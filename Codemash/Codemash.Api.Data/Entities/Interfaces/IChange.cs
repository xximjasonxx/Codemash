namespace Codemash.Api.Data.Entities.Interfaces
{
    public interface IChange
    {
        int ChangeEntityId { set; }
        ChangeAction Action { set; }
        string Key { set; }
        string Value { set; }
    }
}
