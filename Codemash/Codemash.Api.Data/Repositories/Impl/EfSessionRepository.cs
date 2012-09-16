using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class EfSessionRepository : ISessionRepository
    {
        #region Implementation of ISessionRepository

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
            using (var context = new CodemashContext())
            {
                return context.Sessions.ToList();
            }
        }

        /// <summary>
        /// Return all items in the repository matching the given condition
        /// </summary>
        /// <param name="condition">A condition passed as a lambda predicate</param>
        /// <returns></returns>
        public IList<Session> GetAll(Func<Session, bool> condition)
        {
            using (var context = new CodemashContext())
            {
                return context.Sessions.Where(condition).ToList();
            }
        }

        /// <summary>
        /// Apply the changes (add/remove) from a Master Session List
        /// </summary>
        /// <param name="masterSessionList">The session data from the master source</param>
        public void SaveRange(IEnumerable<Session> masterSessionList)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IWriteRepository<Session,int>

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
    }
}
