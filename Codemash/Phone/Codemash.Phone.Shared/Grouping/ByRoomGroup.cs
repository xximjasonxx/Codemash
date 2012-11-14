using System.Collections.Generic;
using System.Linq;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Shared.Grouping
{
    public class ByRoomGroup : IGroup
    {
        #region Implementation of IGroup

        /// <summary>
        /// Groups the Session List by the implementation
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, IList<Session>> Group(IList<Session> sessionList)
        {
            var result = (from session in sessionList
                          orderby session.Room
                          group session by session.Room
                          into Groups
                          select new
                                     {
                                         Groups.Key,
                                         List = Groups.OrderBy(s => s.Title)
                                     }).ToDictionary(d => d.Key, d => (IList<Session>)d.List.ToList());

            return result;
        }

        #endregion
    }
}
