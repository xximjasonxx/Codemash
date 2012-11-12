using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class EfSpeakerRepository : ISpeakerRepository
    {
        #region Implementation of IWriteRepository<Speaker,int>

        /// <summary>
        /// Commit all changes in the repository
        /// </summary>
        public void SaveRange(IEnumerable<Speaker> entityList)
        {
            using (var context = new CodemashContext())
            {
                SaveChanges(context, entityList);
                SaveRemovals(context, entityList);

                context.SaveChanges();
            }
        }

        #endregion

        private void SaveChanges(CodemashContext context, IEnumerable<Speaker> speakers)
        {
            foreach (var entity in speakers)
            {
                var result = context.Speakers.FirstOrDefault(sp => sp.SpeakerId == entity.SpeakerId);
                if (result == null)
                {
                    context.Speakers.Add(entity);
                    continue;
                }

                result.Biography = entity.Biography;
                result.BlogUrl = entity.BlogUrl;
                result.EmailAddress = entity.EmailAddress;
                result.Name = entity.Name;
            }
        }

        private void SaveRemovals(CodemashContext context, IEnumerable<Speaker> speakers)
        {
            var masterSpeakers = speakers.Select(sp => sp.SpeakerId).ToList();
            var existingSpeakers = context.Speakers.Select(sp => sp.SpeakerId).ToList();
            var removals = existingSpeakers.Where(sp => !masterSpeakers.Contains(sp));

            foreach (var speakerId in removals)
            {
                var speaker = context.Speakers.First(sp => sp.SpeakerId == speakerId);
                context.Speakers.Remove(speaker);
            }
        }

        #region Implementation of IReadRepository<Speaker,int>

        /// <summary>
        /// Get an item from the repository by a primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Speaker Get(int id)
        {
            return Get(sp => sp.SpeakerId == id);
        }

        /// <summary>
        /// Return the first instance of T which makes the given condition
        /// </summary>
        /// <param name="condition">The condition passed as a lambda predicate</param>
        /// <returns></returns>
        public Speaker Get(Func<Speaker, bool> condition)
        {
            using (var context = new CodemashContext())
            {
                return context.Speakers.FirstOrDefault(condition);
            }
        }

        /// <summary>
        /// Return all items in the Repository
        /// </summary>
        /// <returns></returns>
        public IList<Speaker> GetAll()
        {
            using (var context = new CodemashContext())
            {
                return context.Speakers.ToList();
            }
        }

        /// <summary>
        /// Return all items in the repository matching the given condition
        /// </summary>
        /// <param name="condition">A condition passed as a lambda predicate</param>
        /// <returns></returns>
        public IList<Speaker> GetAll(Func<Speaker, bool> condition)
        {
            using (var context = new CodemashContext())
            {
                return context.Speakers.Where(condition).ToList();
            }
        }

        #endregion
    }
}
