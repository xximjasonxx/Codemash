using System;
using System.Collections.Generic;
using System.Linq; 
using Codemash.Api.Data.Entities;
using Codemash.Server.Core.Extensions;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class EfSpeakerChangeRepository : ISpeakerChangeRepository
    {
        #region Implementation of IReadRepository<SpeakerChange,int>

        /// <summary>
        /// Get an item from the repository by a primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SpeakerChange Get(int id)
        {
            return Get(sc => sc.SpeakerChangeId == id);
        }

        /// <summary>
        /// Return the first instance of T which makes the given condition
        /// </summary>
        /// <param name="condition">The condition passed as a lambda predicate</param>
        /// <returns></returns>
        public SpeakerChange Get(Func<SpeakerChange, bool> condition)
        {
            using (var context = new CodemashContext())
            {
                return context.SpeakerChanges.FirstOrDefault(condition);
            }
        }

        /// <summary>
        /// Return all items in the Repository
        /// </summary>
        /// <returns></returns>
        public IList<SpeakerChange> GetAll()
        {
            using (var context = new CodemashContext())
            {
                return context.SpeakerChanges.ToList();
            }
        }

        /// <summary>
        /// Return all items in the repository matching the given condition
        /// </summary>
        /// <param name="condition">A condition passed as a lambda predicate</param>
        /// <returns></returns>
        public IList<SpeakerChange> GetAll(Func<SpeakerChange, bool> condition)
        {
            using (var context = new CodemashContext())
            {
                return context.SpeakerChanges.Where(condition).ToList();
            }
        }

        #endregion

        #region Implementation of IWriteRepository<SpeakerChange,int>

        /// <summary>
        /// Commit all changes in the repository
        /// </summary>
        public void SaveRange(IEnumerable<SpeakerChange> entityList)
        {
            int version = 1;
            using (var context = new CodemashContext())
            {
                if (context.SessionChanges.Any())
                    version = context.SessionChanges.Max(sc => sc.Version) + 1;
                entityList.ToList().Apply(sc => sc.Version = version);

                entityList.ToList().ForEach(sc => context.SpeakerChanges.Add(sc));
                context.SaveChanges();
            }
        }

        #endregion

        #region Implementation of ISpeakerChangeRepository

        /// <summary>
        /// Get the latest changeset for Speakers
        /// </summary>
        /// <returns></returns>
        public IList<SpeakerChange> GetLatest()
        {
            using (var context = new CodemashContext())
            {
                if (!context.SpeakerChanges.Any())
                    return new List<SpeakerChange>();

                int maxVersion = context.SpeakerChanges.Max(sc => sc.Version);
                return context.SpeakerChanges.Where(sc => sc.Version == maxVersion).ToList();
            }
        }

        /// <summary>
        /// Get a changeset by version indicator
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public IList<SpeakerChange> GetAll(int version)
        {
            return GetAll(sc => sc.Version == version);
        }

        #endregion
    }
}
