using System.Collections.Generic;
using System.Linq;
using Codemash.Client.Core;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.Common.Models
{
    public class SessionGroup
    {
        public string Title { get; private set; }
        public IList<SessionView> Items { get; private set; }

        public SessionGroup(string title, IEnumerable<Session> sessions)
        {
            Title = title;
            Items = sessions.Select(s => new SessionView
                                             {
                                                 Room = s.Room,
                                                 SessionId = s.SessionId,
                                                 StartsAt = s.Starts.AsTimeDisplay(),
                                                 Technology = s.Technology,
                                                 Title = s.Title
                                             }).ToList();
        }
    }
}
