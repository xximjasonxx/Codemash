using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface IChangeRepository : IReadRepository<Change, int>, IWriteRepository<Change, int>
    {
        /// <summary>
        /// Return the SessionChanges as part of the latest version of changes
        /// </summary>
        /// <returns></returns>
        IEnumerable<Change> GetLatest();

        /// <summary>
        /// Get all the changes for a particular version
        /// </summary>
        /// <param name="changeset">The version of changes to get</param>
        /// <returns></returns>
        IEnumerable<Change> GetAll(int changeset);
    }
}
