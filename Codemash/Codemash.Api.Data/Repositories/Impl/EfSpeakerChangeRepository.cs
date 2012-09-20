using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            using (var context = new CodemashContext())
            {
                var dateCreated = DateTime.UtcNow;
                entityList.ToList().Apply(sc => sc.DateCreated = dateCreated);

                entityList.ToList().ForEach(sc => context.SpeakerChanges.Add(sc));
                context.SaveChanges();
            }
        }

        #endregion
    }
}
