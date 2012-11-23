using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface IChangeRepository : IReadRepository<Change, int>, IWriteRepository<Change, int>
    {
        /// <summary>
        /// Return the unregistered changes for a given client
        /// </summary>
        /// <param name="channelUri">The Uri identifying the channel and thus the client</param>
        /// <returns></returns>
        IEnumerable<Change> GetChangesForChannel(string channelUri);
    }
}
