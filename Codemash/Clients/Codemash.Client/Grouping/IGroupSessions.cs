using System.Collections.Generic;
using Codemash.Client.Data.Entities;
using Codemash.Client.DataModels;

namespace Codemash.Client.Grouping
{
    public interface IGroupSessions
    {
        /// <summary>
        /// Returns the group list as dictated by the underlying implementation
        /// </summary>
        /// <returns></returns>
        IList<SessionGroup> GetGroupedSessions(IEnumerable<Session> sessionList);
    }
}
