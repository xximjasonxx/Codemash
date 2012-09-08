using System;
using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class EfSessionChangeRepository : List<SessionChange>, ISessionChangeRepository
    {
        #region Implementation of IWriteRepository<SessionChange,int>

        /// <summary>
        /// Commit all changes in the repository
        /// </summary>
        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IReadRepository<SessionChange,int>

        /// <summary>
        /// Indicates the Repository should load all data from the local data store
        /// </summary>
        public void Load()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get an item from the repository by a primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SessionChange Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the first instance of T which makes the given condition
        /// </summary>
        /// <param name="condition">The condition passed as a lambda predicate</param>
        /// <returns></returns>
        public SessionChange Get(Func<SessionChange, bool> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all items in the Repository
        /// </summary>
        /// <returns></returns>
        public IList<SessionChange> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all items in the repository matching the given condition
        /// </summary>
        /// <param name="condition">A condition passed as a lambda predicate</param>
        /// <returns></returns>
        public IList<SessionChange> GetAll(Func<SessionChange, bool> condition)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
