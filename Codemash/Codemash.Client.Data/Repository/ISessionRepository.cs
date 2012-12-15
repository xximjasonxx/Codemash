using System.Collections.Generic;
using System.Threading.Tasks;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.Data.Repository
{
    public interface ISessionRepository : IRepository<Session>
    {
        /// <summary>
        /// Get a list of the upcoming sessions that we can show to the user
        /// </summary>
        /// <returns></returns>
        IList<Session> GetUpcomingSessions();

        /// <summary>
        /// Search Session titles for those matching the given search term
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IList<Session> SearchSessions(string value);

        /// <summary>
        /// Save the sessions marked as dirty
        /// </summary>
        void Save();
    }
}
