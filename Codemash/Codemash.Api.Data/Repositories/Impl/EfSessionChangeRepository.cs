using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Server.Core.Extensions;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class EfSessionChangeRepository : ISessionChangeRepository
    {
        #region Implementation of IWriteRepository<SessionChange,int>

        /// <summary>
        /// Commit all changes in the repository
        /// </summary>
        public void SaveRange(IEnumerable<SessionChange> entityList)
        {
            int version = 1;
            using (var context = new CodemashContext())
            {
                // determine the highest present version
                if (context.SessionChanges.Any())
                    version = context.SessionChanges.Max(sc => sc.Version) + 1;

                entityList.ToList().Apply(sc => sc.Version = version);

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
                return GetAll(context, condition);
            }
        }

        /// <summary>
        /// Private version of GetAll which can receive an external context - this allows other methods with existing contexts to share
        /// this functionality
        /// </summary>
        /// <param name="context"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private static IList<SessionChange> GetAll(CodemashContext context, Func<SessionChange, bool> condition)
        {
            return context.SessionChanges.Where(condition).ToList();
        }

        /// <summary>
        /// Return the SessionChanges as part of the latest version of changes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SessionChange> GetLatest()
        {
            using (var context = new CodemashContext())
            {
                // if no sessions exist, return an empty list
                if (!context.SessionChanges.Any())
                    return new List<SessionChange>();

                // latest version is
                var version = context.SessionChanges.Max(sc => sc.Version);
                return GetAll(context, sc => sc.Version == version);
            }
        }

        /// <summary>
        /// Get all the changes for a particular version
        /// </summary>
        /// <param name="version">The version of changes to get</param>
        /// <returns></returns>
        public IEnumerable<SessionChange> GetAll(int version)
        {
            return GetAll(sc => sc.Version == version);
        }

        #endregion
    }
}
