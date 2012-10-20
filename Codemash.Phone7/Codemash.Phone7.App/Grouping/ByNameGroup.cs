using System.Collections.Generic;
using System.Linq;
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
            var groupedSessions = (from s in sessionList
                                   group s by s.Title.Substring(0, 1) into GroupedSessions
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
            finalDictionary.Add("#", sessionList);

            return finalDictionary;
        }
    }
}
