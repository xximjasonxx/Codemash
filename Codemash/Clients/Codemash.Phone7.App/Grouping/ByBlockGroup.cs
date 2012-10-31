using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Phone7.Core;
using Codemash.Phone7.Data.Entities;

namespace Codemash.Phone7.App.Grouping
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
                                   group s by s.Starts.AsBlockDisplay() into GroupedSessions
                                   select new
                                              {
                                                  Key = GroupedSessions.Key,
                                                  Value = GroupedSessions
                                              }).ToList();

            return groupedSessions.ToDictionary(kv => kv.Key, kv => (IList<Session>)kv.Value.ToList());
        }

        #endregion
    }
}
