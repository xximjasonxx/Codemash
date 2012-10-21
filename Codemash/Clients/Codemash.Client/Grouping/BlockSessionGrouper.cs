using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Codemash.Client.Code;
using Codemash.Client.Core;
using Codemash.Client.Data.Entities;
using Codemash.Client.DataModels;

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
            Regex regex = new Regex(@"\w");
            var groupedSet = (from s in _sessionList
                              orderby s.Starts
                              group s by s.Starts.AsBlockDisplay() into BlockSessions
                              select new
                                         {
                                            Key = BlockSessions.Key,
                                            Value = BlockSessions.OrderBy(s => regex.Matches(s.Title)[0].Value).ToList()
                                         }).ToDictionary(kv => kv.Key, kv => kv.Value.ToList());

            var combinedList = new List<IListItem>();
            foreach (var kv in groupedSet)
            {
                combinedList.Add(new SessionListGroup {GroupTitle = kv.Key});
                combinedList.AddRange(kv.Value.Select(s => new SessionListView {SessionTitle = s.Title, SessionId = s.SessionId}));
            }

            return combinedList;
        }

        #endregion
    }
}
