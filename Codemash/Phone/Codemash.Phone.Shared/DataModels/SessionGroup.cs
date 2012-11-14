using System.Collections;
using System.Collections.Generic;

namespace Codemash.Phone.Shared.DataModels
{
    public class SessionGroup : List<SessionListView>
    {
        public string Key { get; private set; }

        public SessionGroup(string title, IEnumerable<SessionListView> items)
        {
            Key = title;
            AddRange(items);
        }
    }
}
