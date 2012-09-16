using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class EfSessionChangeRepository : RepositoryBase<SessionChange>, ISessionChangeRepository
    {
        #region Implementation of IWriteRepository<SessionChange,int>

        /// <summary>
        /// Commit all changes in the repository
        /// </summary>
        public void Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mark an entry in the repository as removed
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
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
        /// <param name="id">The SessionChangeID to key on</param>
        /// <returns>The Session Change with the given SessionChangeID</returns>
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

        #region Overrides of RepositoryBase<SessionChange>

        /// <summary>
        /// Name of the repository
        /// </summary>
        public override string RepositoryName
        {
            get { return "SessionChange"; }
        }

        #endregion
    }
}
