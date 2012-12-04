using System.Collections.Generic;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Data.Repository
{
    public interface ISessionRepository : IRepository<Session>
    {
        /// <summary>
        /// Return the list of upcoming sessions from the repository
        /// </summary>
        IList<Session> GetUpcomingSessions();

        /// <summary>
        /// Return a listing of all of the sessions in the repository
        /// </summary>
        /// <returns></returns>
        IList<Session> GetAllSessions();

        /// <summary>
        /// Return sessions which contain the term
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        IList<Session> FindSessions(string searchTerm);

        /// <summary>
        /// Return a list of sessions that are marked as favorites
        /// </summary>
        /// <returns></returns>
        IList<Session> GetFavoriteSessions();

        /// <summary>
        /// Update the favorite status in the local database
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="favoriteStatus"></param>
        void UpdateFavoriteStatus(long sessionId, bool favoriteStatus);

        /// <summary>
        /// Save the current state of the repository
        /// </summary>
        void SaveChanges();
    }
}
