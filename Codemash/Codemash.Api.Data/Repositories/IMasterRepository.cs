using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface IMasterRepository
    {
        /// <summary>
        /// Return all sessions from the Master Datasource
        /// </summary>
        IList<MasterSession> GetAllSessions();

        /// <summary>
        /// Return all speakers from the Master Datasource
        /// </summary>
        IList<MasterSpeaker> GetAllSpeakers();
    }
}
