using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Codemash.Client.Common.Models;
using Codemash.Client.Core;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.Common.Grouping.Impl
{
    public class BlockSessionGrouper : IGroupSessions
    {
        #region Implementation of IGroupSessions

        /// <summary>
        /// Returns the group list as dictated by the underlying implementation
        /// </summary>
        /// <returns></returns>
        public IList<SessionGroup> GetGroupedSessions(IEnumerable<Session> sessionList)
        {
            Regex regex = new Regex(@"\w");
            var groupedSet = (from s in sessionList
                              orderby s.Starts
                              group s by s.Starts.AsBlockDisplay() into BlockSessions
                              select new
                                         {
                                            Key = BlockSessions.Key,
                                            Value = BlockSessions.OrderBy(s => regex.Matches(s.Title)[0].Value).ToList()
                                         }).ToDictionary(kv => kv.Key, kv => kv.Value.ToList());
            return groupedSet.Select(kv => new SessionGroup(kv.Key, kv.Value)).ToList();
        }

        #endregion
    }
}
