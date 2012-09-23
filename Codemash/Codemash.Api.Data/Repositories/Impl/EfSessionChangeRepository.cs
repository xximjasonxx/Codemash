using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Server.Core.Extensions;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class EfSessionChangeRepository : RepositoryBase, ISessionChangeRepository
    {
        #region Implementation of IWriteRepository<SessionChange,int>

        /// <summary>
        /// Commit all changes in the repository
        /// </summary>
        public void SaveRange(IEnumerable<SessionChange> entityList)
        {
            var blockId = GetBlockId();
            using (var context = new CodemashContext())
            {
                entityList.ToList().Apply(sc => sc.Block = blockId);

                entityList.ToList().ForEach(sc => context.SessionChanges.Add(sc));
                context.SaveChanges();
            }
        }

        #endregion

        #region Implementation of IReadRepository<SessionChange,int>

        /// <summary>
        /// Get an item from the repository by a primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SessionChange Get(int id)
        {
            return Get(sc => sc.SessionChangeId == id);
        }

        /// <summary>
        /// Return the first instance of T which makes the given condition
        /// </summary>
        /// <param name="condition">The condition passed as a lambda predicate</param>
        /// <returns></returns>
        public SessionChange Get(Func<SessionChange, bool> condition)
        {
            using (var context = new CodemashContext())
            {
                return context.SessionChanges.FirstOrDefault(condition);
            }
        }

        /// <summary>
        /// Return all items in the Repository
        /// </summary>
        /// <returns></returns>
        public IList<SessionChange> GetAll()
        {
            using (var context = new CodemashContext())
            {
                return context.SessionChanges.ToList();
            }
        }

        /// <summary>
        /// Return all items in the repository matching the given condition
        /// </summary>
        /// <param name="condition">A condition passed as a lambda predicate</param>
        /// <returns></returns>
        public IList<SessionChange> GetAll(Func<SessionChange, bool> condition)
        {
            using (var context = new CodemashContext())
            {
                return context.SessionChanges.Where(condition).ToList();
            }
        }

        #endregion
    }
}
