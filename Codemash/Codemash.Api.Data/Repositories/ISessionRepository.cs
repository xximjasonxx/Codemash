using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface ISessionRepository : IReadRepository<Session, int>, IWriteRepository<Session, int>
    {
        /// <summary>
        /// Apply the changes (add/remove) from a Master Session List
        /// </summary>
        /// <param name="masterSessionList">The session data from the master source</param>
        void SaveRange(IEnumerable<Session> masterSessionList);
    }
}
