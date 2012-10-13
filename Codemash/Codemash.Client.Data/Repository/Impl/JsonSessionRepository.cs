using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Codemash.Client.Core;
using Codemash.Client.Data.Entities;
using Newtonsoft.Json.Linq;

namespace Codemash.Client.Data.Repository.Impl
{
    public class JsonSessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        #region Implementation of IRepository<Session>

        /// <summary>
        /// Load the repository with data
        /// </summary>
        public async void Load()
        {
            const string downloadUrl = "http://dl.dropbox.com/u/13029365/DevLink2012_Sessions.json";
            using (var client = new HttpClient())
            {
                var responseString = await client.GetStringAsync(downloadUrl);
                var jsonArray = JArray.Parse(responseString);

                var sessions = (from ji in jsonArray.AsJEnumerable()
                                select new Session
                                           {
                                               Title = new StringWrapper(ji["Title"]).ToString(),
                                               Abstract = new StringWrapper(ji["Abstract"]).ToString(),
                                               Level = new StringWrapper(ji["Level"]).ToString(),
                                               Track = new StringWrapper(ji["Track"]).ToString(),
                                               Room = new StringWrapper(ji["Room"]).ToString(),
                                               SpeakerId = new StringWrapper(ji["Speaker"]["SpeakerId"]).ToString().AsInt(),
                                               SessionId = new StringWrapper(ji["SessionId"]).ToString().AsInt(),
                                               SpeakerName = ji["Speaker"]["FirstName"].ToString() + " " + ji["Speaker"]["LastName"].ToString(),
                                               Starts = ji["StartTime"].ToString().AsDateTime(true),
                                               Ends = ji["EndTime"].ToString().AsDateTime(true)
                                           }).ToList();

                AddRange(sessions);
                if (LoadCompleted != null)
                    LoadCompleted(this, new EventArgs());
            }
        }

        public event EventHandler LoadCompleted;

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
            return Repository.Take(10).ToList();
        }

        #endregion
    }
}
