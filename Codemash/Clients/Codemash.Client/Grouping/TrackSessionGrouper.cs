using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Codemash.Client.Code;
using Codemash.Client.Data.Entities;
using Codemash.Client.DataModels;

namespace Codemash.Client.Grouping
{
    public class TrackSessionGrouper : IGroupSessions
    {
        private readonly IList<Session> _sessions; 

        public TrackSessionGrouper(IList<Session> sessionList)
        {
            _sessions = sessionList;
        }

        #region Implementation of IGroupSessions

        /// <summary>
        /// Returns the group list as dictated by the underlying implementation
        /// </summary>
        /// <returns></returns>
        public IList<SessionGroup> GetGroupedList()
        {
            Regex regex = new Regex(@"\w");
            var groupedSet = (from s in _sessions
                              orderby s.Technology
                              group s by s.Technology
                              into TrackGrouping
                              select new
                                         {
                                             Key = TrackGrouping.Key,
                                             Value = TrackGrouping.OrderBy(s => regex.Matches(s.Title)[0].Value)
                                         }).ToDictionary(kv => kv.Key, kv => kv.Value.ToList());
            return groupedSet.Select(kv => new SessionGroup(kv.Key, kv.Value)).ToList();
        }

        #endregion
    }
}
