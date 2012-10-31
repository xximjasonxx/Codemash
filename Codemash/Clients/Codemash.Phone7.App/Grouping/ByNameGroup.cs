using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Codemash.Phone7.Core;
using Codemash.Phone7.Data.Entities;

namespace Codemash.Phone7.App.Grouping
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
            Regex regex = new Regex(@"\w");
            var groupedSessions = (from s in sessionList
                                   group s by regex.Matches(s.Title)[0].Value into GroupedSessions
                                   select new
                                   {
                                       Key = GroupedSessions.Key,
                                       Value = GroupedSessions
                                   }).ToList();

            var finalDictionary = groupedSessions.ToDictionary(kv => kv.Key, kv => (IList<Session>)kv.Value.ToList());
            return CondenseNumerics(finalDictionary);
        }

        #endregion

        private IDictionary<string, IList<Session>> CondenseNumerics(IDictionary<string, IList<Session>> dictionary)
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
