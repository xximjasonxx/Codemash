using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codemash.Client.Core;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.DataModels
{
    public class SessionGroup
    {
        public string Title { get; private set; }
        public IList<SessionTileView> Items { get; private set; }

        public SessionGroup(string title, IEnumerable<Session> sessions)
        {
            Title = title;
            Items = sessions.Select(s => new SessionTileView
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
