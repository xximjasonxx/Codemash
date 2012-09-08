
namespace Codemash.Api.Data.Entities
{
    public class SessionChange : EntityBase
    {
        public int SessionChangeId { get; set; }
        public int SessionId { get; set; }
        public SessionChangeAction Action { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
