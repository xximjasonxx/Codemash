using System;
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
            var blocks = Repository.Select(s => s.Starts).Distinct().ToList();
            if (blocks.Count(d => d >= DateTime.Now) == 0)
            {
                return new List<Session>();
            }

            var upcomingBlock = blocks.OrderBy(d => d).FirstOrDefault(d => d >= DateTime.Now);
            return Repository.Where(s => s.Starts == upcomingBlock).OrderBy(s => s.Title).ToList();
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

        /// <summary>
        /// Gets whether this is the first load of the application
        /// </summary>
        public bool IsFirstLoad { get { return !TableExists; } }

        #endregion

        #region Overrides of RepositoryBase<Session>

        protected override string DownloadUrl
        {
            get { return "http://codemashdelta.azurewebsites.net/api/Session"; }
        }

        protected override Session CreateEntity(JToken jToken)
        {
            var session = new Session
                              {
                                  Abstract = new StringWrapper(jToken["Abstract"]).ToString(),
                                  Difficulty = new StringWrapper(jToken["Level"]).ToString(),
                                  SessionId = new StringWrapper(jToken["SessionId"]).ToString().AsInt(),
                                  SpeakerId = new StringWrapper(jToken["SpeakerId"]).ToString().AsInt(),
                                  Room = new StringWrapper(jToken["Room"]).ToString(),
                                  Technology = new StringWrapper(jToken["Track"]).ToString(),
                                  Title = new StringWrapper(jToken["Title"]).ToString(),
                                  Starts = new StringWrapper(jToken["Start"]).ToString().AsDateTime(),
                                  Ends = new StringWrapper(jToken["End"]).ToString().AsDateTime()
                              };

            return session;
        }

        #endregion
    }
}
