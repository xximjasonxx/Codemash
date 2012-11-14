using System.Collections;
using System.Collections.Generic;

namespace Codemash.Phone.Shared.DataModels
{
    public class SessionGroup : List<SessionListView>
    {
        public string Title { get; private set; }

        public SessionGroup(string title, IEnumerable<SessionListView> items)
        {
            Title = title;
            AddRange(items);
        }
    }
}
