using System.Collections.Generic;
using System.Linq;
using Codemash.Client.Data.Entities;
using Codemash.Client.DataModels;

namespace Codemash.Client.Grouping
{
    public class RoomSessionGrouper : IGroupSessions
    {
        #region Implementation of IGroupSessions

        /// <summary>
        /// Returns the group list as dictated by the underlying implementation
        /// </summary>
        /// <returns></returns>
        public IList<SessionGroup> GetGroupedSessions(IEnumerable<Session> sessionList)
        {
            return (from session in sessionList
                    orderby session.Room
                    group session by session.Room into Groups
                    select new SessionGroup(Groups.Key, Groups.OrderBy(s => s.Title))).ToList();
        }

        #endregion
    }
}
