using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Phone7.Data.Entities;

namespace Codemash.Phone7.Data.Repository
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
    }
}
