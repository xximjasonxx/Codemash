﻿using System.Collections.Generic;
using System.Linq;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Shared.Grouping
{
    public class ByTechGroup : IGroup
    {
        #region Implementation of IGroup

        /// <summary>
        /// Groups the Session List by the implementation
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, IList<Session>> Group(IList<Session> sessionList)
        {
            var groupedSessions = (from s in sessionList
                                   where !string.IsNullOrEmpty(s.Technology)
                                   group s by s.Technology into GroupedSessions
                                   select new
                                   {
                                       Key = GroupedSessions.Key,
                                       Value = GroupedSessions
                                   }).ToList();

            return groupedSessions.OrderBy(gs => gs.Key).ToDictionary(kv => kv.Key, kv => (IList<Session>)kv.Value.ToList());
        }

        #endregion
    }
}
