using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codemash.Client.Core;
using Codemash.Client.Core.Ex;
using Codemash.Client.Data.Entities;
using Newtonsoft.Json.Linq;

namespace Codemash.Client.Data.Repository.Impl
{
    public class JsonSessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        #region Implementation of IRepository<Session>

        /// <summary>
        /// Return an item from the repository by an ID value
        /// </summary>
        /// <param name="id">The ID value, should be unique</param>
        /// <returns></returns>
        public Session Get(int id)
        {
            return Repository.FirstOrDefault(s => s.SessionId == id);
        }

        /// <summary>
        /// return a list of all items in the repository
        /// </summary>
        /// <returns></returns>
        public IList<Session> GetAll()
        {
            return Repository;
        }

        #endregion

        #region Implementation ISessionRepository

        /// <summary>
        /// Get a list of the upcoming sessions that we can show to the user
        /// </summary>
        /// <returns></returns>
        public IList<Session> GetUpcomingSessions()
        {
            return Repository.OrderBy(s => s.Title).Take(10).ToList();
        }

        /// <summary>
        /// Search Session titles for those matching the given search term
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public IList<Session> SearchSessions(string searchTerm)
        {
            if (!TableExists)
                throw new BaseDataNotAvailableException();

            if (Repository.Count == 0)
                LoadRepositoryFromDatabase();

            return Repository.Where(s => s.Title.ToLower().Contains(searchTerm.ToLower()))
                .OrderBy(s => s.Title).ToList();
        }

        #endregion

        #region Overrides of RepositoryBase<Session>

        protected override string DownloadUrl
        {
            get { return "http://dl.dropbox.com/u/13029365/codemash_sessions.json"; }
        }

        protected override Session CreateEntity(JToken jToken)
        {
            return new Session
                       {
                           Abstract = new StringWrapper(jToken["Abstract"]).ToString(),
                           Difficulty = new StringWrapper(jToken["Difficulty"]).ToString(),
                           SessionId = new StringWrapper(jToken["Title"]).ToString().AsKey(),
                           SpeakerId = new StringWrapper(jToken["SpeakerName"]).ToString().AsKey(),
                           Technology = new StringWrapper(jToken["Technology"]).ToString(),
                           Title = new StringWrapper(jToken["Title"]).ToString()
                       };
        }

        #endregion
    }
}
