using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
