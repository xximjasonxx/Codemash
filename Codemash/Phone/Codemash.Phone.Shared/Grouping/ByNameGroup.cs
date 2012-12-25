using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Codemash.Phone.Core;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Shared.Grouping
{
    public class ByNameGroup : IGroup
    {
        #region Implementation of IGroup

        /// <summary>
        /// Groups the Session List by the implementation
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, IList<Session>> Group(IList<Session> sessionList)
        {
            // gather all sessions by the first character
            var rawList = (from s in sessionList
                           group s by Regex.Match(s.Title, "[A-Za-z0-9]{1}").Value.ToUpper()
                           into GroupedSessions
                           select new
                                      {
                                          Key = GroupedSessions.Key,
                                          Sessions = GroupedSessions
                                      }).OrderBy(n => n.Key).ToList();

            // filter the base result for those sessions starting with a letter
            var alphaSessions = rawList.Where(r => r.Key.AsInt() < 0).ToDictionary(r => r.Key, r => r.Sessions.ToList());

            // group the sessions that start with a number into their own key value pair
            var numericSessions = new List<Session>();
            rawList.Where(r => r.Key.AsInt() >= 0).ToList().ForEach(r => numericSessions = numericSessions.Concat(r.Sessions).ToList());

            // aggregate the final
            var result = new Dictionary<string, IList<Session>>();
            if (numericSessions.Count > 0)
                result.Add("#", numericSessions);

            alphaSessions.ToList().ForEach(kv => result.Add(kv.Key, kv.Value));

            return result;
        }

        #endregion
    }
}
