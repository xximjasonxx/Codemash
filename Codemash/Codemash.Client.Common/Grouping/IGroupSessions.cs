using System.Collections.Generic;
using Codemash.Client.Common.Models;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.Common.Grouping
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
