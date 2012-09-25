using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface ISpeakerChangeRepository : IReadRepository<SpeakerChange, int>, IWriteRepository<SpeakerChange, int>
    {
        /// <summary>
        /// Get the latest changeset for Speakers
        /// </summary>
        /// <returns></returns>
        IList<SpeakerChange> GetLatest();

        /// <summary>
        /// Get a changeset by version indicator
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        IList<SpeakerChange> GetAll(int version);
    }
}
