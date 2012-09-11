using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class EfSessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        #region Implementation of ISessionRepository

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
        public Session Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the first instance of T which makes the given condition
        /// </summary>
        /// <param name="condition">The condition passed as a lambda predicate</param>
        /// <returns></returns>
        public Session Get(Func<Session, bool> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all items in the Repository
        /// </summary>
        /// <returns></returns>
        public IList<Session> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all items in the repository matching the given condition
        /// </summary>
        /// <param name="condition">A condition passed as a lambda predicate</param>
        /// <returns></returns>
        public IList<Session> GetAll(Func<Session, bool> condition)
        {
            throw new NotImplementedException();
        }

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

        /// <summary>
        /// Apply the changes (add/remove) from a Master Session List
        /// </summary>
        /// <param name="masterSessionList">The session data from the master source</param>
        public void ApplyRange(IEnumerable<Session> masterSessionList)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Overrides of RepositoryBase<Session>

        /// <summary>
        /// Name of the repository
        /// </summary>
        public override string RepositoryName
        {
            get { return "Session"; }
        }

        #endregion
    }
}
