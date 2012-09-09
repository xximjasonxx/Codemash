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
            var saveTime = DateTime.UtcNow;
            using (var context = new CodemashSessionsContext())
            {
                foreach (var item in this.Where(item => item.IsDirty))
                {
                    item.ChangeTime = saveTime;
                    context.SessionChanges.Add(item);
                }

                context.SaveChanges();
            }
        }

        #endregion

        #region Implementation of IReadRepository<SessionChange,int>

        /// <summary>
        /// Indicates the Repository should load all data from the local data store
        /// </summary>
        public void Load()
        {
            var loadedChangeIds = this.Select(sc => sc.SessionChangeId);
            using (var context = new CodemashSessionsContext())
            {
                context.SessionChanges.ToList().ForEach(sc =>
                    {
                        if (!loadedChangeIds.Contains(sc.SessionChangeId))
                        {
                            sc.MarkAsExisting();
                            Add(sc);
                        }
                    });
            }

            RepositoryHasLoaded();
        }

        /// <summary>
        /// Get an item from the repository by a primary key
        /// </summary>
        /// <param name="id">The SessionChangeId to key on</param>
        /// <returns>The Session Change with the given SessionChangeId</returns>
        public SessionChange Get(int id)
        {
            CheckRepositoryHasLoaded();
            return this.FirstOrDefault(sc => sc.SessionChangeId == id);
        }

        /// <summary>
        /// Return the first instance of T which makes the given condition
        /// </summary>
        /// <param name="condition">The condition passed as a lambda predicate</param>
        /// <returns></returns>
        public SessionChange Get(Func<SessionChange, bool> condition)
        {
            CheckRepositoryHasLoaded();
            return this.FirstOrDefault(condition);
        }

        /// <summary>
        /// Return all items in the Repository
        /// </summary>
        /// <returns></returns>
        public IList<SessionChange> GetAll()
        {
            CheckRepositoryHasLoaded();
            return this;
        }

        /// <summary>
        /// Return all items in the repository matching the given condition
        /// </summary>
        /// <param name="condition">A condition passed as a lambda predicate</param>
        /// <returns></returns>
        public IList<SessionChange> GetAll(Func<SessionChange, bool> condition)
        {
            CheckRepositoryHasLoaded();
            return this.Where(condition).ToList();
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
