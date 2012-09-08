using System;
using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class BlobSessionChangeRepository : ISessionChangeRepository
    {
        #region Implementation of IWriteRepository<SessionChange,int>

        /// <summary>
        /// Add items to the Repository
        /// </summary>
        /// <param name="items">The items to be added to the repository</param>
        public void AddRange(IEnumerable<SessionChange> items)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Commit all changes in the repository
        /// </summary>
        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
