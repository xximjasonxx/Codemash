using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface ISessionRepository : IReadRepository<Session, int>, IWriteRepository<Session, int>
    {
        /// <summary>
        /// Make a modification to an existing session
        /// </summary>
        /// <param name="change"> </param>
        void ModifySession(SessionChange change);

        /// <summary>
        /// Adds a session based on data from a SessionChange occurence
        /// </summary>
        /// <param name="sessionInformation"></param>
        void AddSession(IList<SessionChange> sessionInformation);
    }
}
