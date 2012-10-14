using System.Collections.Generic;
using System.Linq;
using Codemash.Client.Core;
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

        #endregion

        #region Overrides of RepositoryBase<Session>

        protected override string DownloadUrl
        {
            get { return "http://dl.dropbox.com/u/13029365/DevLink2012_Sessions.json"; }
        }

        protected override Session CreateEntity(JToken ji)
        {
            return new Session
                       {
                           Title = new StringWrapper(ji["Title"]).ToString(),
                           Abstract = new StringWrapper(ji["Abstract"]).ToString(),
                           Level = new StringWrapper(ji["Level"]).ToString(),
                           Track = new StringWrapper(ji["Track"]).ToString(),
                           Room = new StringWrapper(ji["Room"]).ToString(),
                           SpeakerId = new StringWrapper(ji["Speaker"]["SpeakerId"]).ToString().AsInt(),
                           SessionId = new StringWrapper(ji["SessionId"]).ToString().AsInt(),
                           Starts = ji["StartTime"].ToString().AsDateTime(true),
                           Ends = ji["EndTime"].ToString().AsDateTime(true)
                       };
        }

        #endregion
    }
}
