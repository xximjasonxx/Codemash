using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface ISessionChangeRepository : IWriteRepository<SessionChange, int>, IReadRepository<SessionChange, int>
    {
        /// <summary>
        /// Return the SessionChanges as part of the latest version of changes
        /// </summary>
        /// <returns></returns>
        IEnumerable<SessionChange> GetLatest();

        /// <summary>
        /// Get all the changes for a particular version
        /// </summary>
        /// <param name="version">The version of changes to get</param>
        /// <returns></returns>
        IEnumerable<SessionChange> GetAll(int version);
    }
}
