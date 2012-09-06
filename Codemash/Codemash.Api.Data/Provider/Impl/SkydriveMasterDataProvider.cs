using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Provider.Impl
{
    public class SkydriveMasterDataProvider : IMasterDataProvider
    {
        #region Implementation of IMasterDataProvider

        /// <summary>
        /// Return all sessions from the Master Datasource
        /// </summary>
        public IList<Session> GetAllSessions()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all speakers from the Master Datasource
        /// </summary>
        public IList<Speaker> GetAllSpeakers()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
