using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Codemash.Client.Code;
using Codemash.Client.Data.Entities;
using Codemash.Client.Core;
using Codemash.Client.DataModels;

namespace Codemash.Client.Grouping
{
    public class AlphanumericSessionGrouper : IGroupSessions
    {
        private readonly IList<Session> _sessionList; 

        public AlphanumericSessionGrouper(IList<Session> sessionList)
        {
            _sessionList = sessionList;
        }

        #region Implementation of IGroupSessions

        /// <summary>
        /// Returns the group list as dictated by the underlying implementation
        /// </summary>
        /// <returns></returns>
        public IList<SessionGroup> GetGroupedList()
        {
            var groupedSet = GetGroupedSet(_sessionList);
            return groupedSet.Select(kv => new SessionGroup(kv.Key, kv.Value)).OrderBy(sg => sg.Title).ToList();
        }

        #endregion

        private Dictionary<string, List<Session>> GetGroupedSet(IEnumerable<Session> sessionList)
        {
            Regex regex = new Regex(@"\w");
            var groupedResult = (from s in sessionList
                                 group s by regex.Matches(s.Title)[0].Value into GroupedSessions
                                 select new
                                            {
                                                Key = GroupedSessions.Key,
                                                Value = GroupedSessions
                                            });

            var resultDictionary = groupedResult
                .ToDictionary(kv => kv.Key, kv => kv.Value.OrderBy(s => regex.Matches(s.Title)[0].Value).ToList());
            return CondenseNumerics(resultDictionary);
        }

        private Dictionary<string, List<Session>> CondenseNumerics(Dictionary<string, List<Session>> dictionary)
        {
            var numerics = dictionary.Where(kv => kv.Key.AsInt() > 0);
            var sessionList = new List<Session>();
            sessionList = numerics.Aggregate(sessionList, (current, kv) => current.Concat(kv.Value).ToList());

            var finalDictionary = dictionary.Where(kv => kv.Key.AsInt() == int.MinValue)
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            if (sessionList.Count > 0)
                finalDictionary.Add("#", sessionList);

            return finalDictionary;
        }
    }
}
