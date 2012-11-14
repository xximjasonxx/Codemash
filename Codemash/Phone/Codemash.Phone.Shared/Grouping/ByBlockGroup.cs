using System.Collections.Generic;
using System.Linq;
using Codemash.Phone.Core;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Shared.Grouping
{
    public class ByBlockGroup : IGroup
    {
        #region Implementation of IGroup

        /// <summary>
        /// Groups the Session List by the implementation
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, IList<Session>> Group(IList<Session> sessionList)
        {
            var groupedSessions = (from s in sessionList
                                   group s by s.Starts.AsDateTime().AsBlockDisplay() into GroupedSessions
                                   select new
                                              {
                                                  GroupedSessions.Key,
                                                  Value = GroupedSessions
                                              }).ToList();

            return groupedSessions.ToDictionary(kv => kv.Key, kv => (IList<Session>)kv.Value.ToList());
        }

        #endregion
    }
}
