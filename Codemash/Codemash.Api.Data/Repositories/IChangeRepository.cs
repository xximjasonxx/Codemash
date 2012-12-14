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

        /// <summary>
        /// Return changes for a client based on their client Id value
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        IEnumerable<Change> GetChangesForClient(int clientId);
    }
}
