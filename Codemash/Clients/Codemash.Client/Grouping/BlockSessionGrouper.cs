using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codemash.Client.Code;
using Codemash.Client.Core;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.Grouping
{
    public class BlockSessionGrouper : IGroupSessions
    {
        private readonly IList<Session> _sessionList;

        public BlockSessionGrouper(IList<Session> sessionList)
        {
            _sessionList = sessionList;
        }

        #region Implementation of IGroupSessions

        /// <summary>
        /// Returns the group list as dictated by the underlying implementation
        /// </summary>
        /// <returns></returns>
        public IList<IListItem> GetGroupedList()
        {
            var groupedSet = (from s in _sessionList
                              orderby s.Starts
                              group s by s.Starts.AsBlockDisplay() into BlockSessions
                              select new
                                         {
                                            Key = BlockSessions.Key,
                                            Value = BlockSessions.ToList()
                                         }).ToDictionary(kv => kv.Key, kv => kv.Value.ToList());

            var combinedList = new List<IListItem>();
            foreach (var kv in groupedSet)
            {
                combinedList.Add(new SessionListGroup {GroupTitle = kv.Key});
                combinedList.AddRange(kv.Value.Select(s => new SessionListView {SessionTitle = s.Title}));
            }

            return combinedList;
        }

        #endregion
    }
}
