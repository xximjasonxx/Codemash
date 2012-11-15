using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Server.Core.Extensions;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class EfChangeRepository : IChangeRepository
    {
        #region Implementation of IReadRepository<SessionChange,int>

        /// <summary>
        /// Get an item from the repository by a primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Change Get(int id)
        {
            return Get(sc => sc.ChangeId == id);
        }

        /// <summary>
        /// Return the first instance of T which makes the given condition
        /// </summary>
        /// <param name="condition">The condition passed as a lambda predicate</param>
        /// <returns></returns>
        public Change Get(Func<Change, bool> condition)
        {
            using (var context = new CodemashContext())
            {
                return context.Changes.FirstOrDefault(condition);
            }
        }

        /// <summary>
        /// Return all items in the Repository
        /// </summary>
        /// <returns></returns>
        public IList<Change> GetAll()
        {
            using (var context = new CodemashContext())
            {
                return context.Changes.ToList();
            }
        }

        /// <summary>
        /// Return all items in the repository matching the given condition
        /// </summary>
        /// <param name="condition">A condition passed as a lambda predicate</param>
        /// <returns></returns>
        public IList<Change> GetAll(Func<Change, bool> condition)
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
        private static IList<Change> GetAll(CodemashContext context, Func<Change, bool> condition)
        {
            return context.Changes.Where(condition).ToList();
        }

        /// <summary>
        /// Return the SessionChanges as part of the latest version of changes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Change> GetLatest()
        {
            using (var context = new CodemashContext())
            {
                // if no sessions exist, return an empty list
                if (!context.Changes.Any())
                    return new List<Change>();

                // latest version is
                var version = context.Changes.Max(sc => sc.Changeset);
                return GetAll(context, sc => sc.Changeset == version);
            }
        }

        /// <summary>
        /// Get all the changes for a particular version
        /// </summary>
        /// <param name="version">The version of changes to get</param>
        /// <returns></returns>
        public IEnumerable<Change> GetAll(int version)
        {
            return GetAll(sc => sc.Changeset == version);
        }

        #endregion

        #region Implementation of IWriteRepository<Change,int>

        /// <summary>
        /// Commit all changes in the repository
        /// </summary>
        public void SaveRange(IEnumerable<Change> entityList)
        {
            // establish the changeset number
            int changeset = 1;
            using (var context = new CodemashContext())
            {
                if (context.Changes.Any())
                    changeset = context.Changes.Max(c => c.Changeset) + 1;

                foreach (var change in entityList)
                {
                    change.Changeset = changeset;
                    context.Changes.Add(change);
                }

                context.SaveChanges();
            }
        }

        #endregion
    }
}
