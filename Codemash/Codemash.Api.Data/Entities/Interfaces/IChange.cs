namespace Codemash.Api.Data.Entities.Interfaces
{
    public interface IChange
    {
        int ChangeEntityID { set; }
        ChangeAction Action { set; }
        string Key { set; }
        string Value { set; }
    }
}
