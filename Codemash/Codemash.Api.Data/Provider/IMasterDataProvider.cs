using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Provider
{
    public interface IMasterDataProvider
    {
        /// <summary>
        /// Return all sessions from the Master Datasource
        /// </summary>
        IList<Session> GetAllSessions();

        /// <summary>
        /// Return all speakers from the Master Datasource
        /// </summary>
        IList<Speaker> GetAllSpeakers();
    }
}
