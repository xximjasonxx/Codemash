using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the first instance of T which makes the given condition
        /// </summary>
        /// <param name="condition">The condition passed as a lambda predicate</param>
        /// <returns></returns>
        public SpeakerChange Get(Func<SpeakerChange, bool> condition)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion
    }
}
