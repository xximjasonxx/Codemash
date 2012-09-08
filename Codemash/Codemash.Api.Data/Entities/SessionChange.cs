
namespace Codemash.Api.Data.Entities
{
    public class SessionChange : EntityBase
    {
        private int _sessionId;
        private SessionChangeAction _action;
        private string _key;
        private string _value;

        public int SessionChangeId { get; set; }

        public int SessionId
        {
            get { return _sessionId; }
            set
            {
                ValueChanged();
                _sessionId = value;
            }
        }

        public SessionChangeAction Action
        {
            get { return _action; }
            set
            {
                ValueChanged();
                _action = value;
            }
        }

        public string Key
        {
            get { return _key; }
            set
            {
                ValueChanged();
                _key = value;
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                ValueChanged();
                _value = value;
            }
        }
    }
}
