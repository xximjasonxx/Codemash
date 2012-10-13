using Codemash.Client.Data.Entities;

namespace Codemash.Client.Parameters
{
    public class SessionParameter
    {
        public Session Value { get; private set; }

        public SessionParameter(Session session)
        {
            Value = session;
        }
    }
}
