using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Data
{
    public class SessionGroup
    {
        public string GroupTitle { get; private set; }
        public ObservableCollection<Session> Sessions { get; set; }

        public SessionGroup(string groupTitle)
        {
            GroupTitle = groupTitle;
            Sessions = new ObservableCollection<Session>();
        }
    }
}
