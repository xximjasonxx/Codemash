using System.Collections.Generic;
using System.Linq;
using Codemash.Phone7.Core;
using Codemash.Phone7.Data.Common;
using Codemash.Phone7.Data.Entities;
using Newtonsoft.Json.Linq;

namespace Codemash.Phone7.Data.Repository.Impl
{
    public class JsonSessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        #region Overrides of RepositoryBase<Session>

        protected override string DownloadUrl
        {
            get { return "http://dl.dropbox.com/u/13029365/codemash_sessions.json"; }
        }

        protected override Session CreateObject(JToken jToken)
        {
            return new Session()
                       {
                           Abstract = new StringWrapper(jToken["Abstract"]).ToString(),
                           Difficulty = new StringWrapper(jToken["Difficulty"]).ToString(),
                           EventType = new StringWrapper(jToken["EventType"]).ToString(),
                           SessionId = new StringWrapper(jToken["URI"]).ToString().AsKey(),
                           SpeakerId = new StringWrapper(jToken["SpeakerURI"]).ToString().AsKey(),
                           Technology = new StringWrapper(jToken["Technology"]).ToString(),
                           Title = new StringWrapper(jToken["Title"]).ToString()
                       };
        }

        /// <summary>
        /// Instructs the repository to Save all dirty records
        /// </summary>
        public override void Save()
        {
            if (Repository.Count(it => it.IsDirty) > 0)
            {
                using (var db = GetContext())
                {
                    foreach (var item in Repository.Where(it => it.IsDirty))
                    {
                        switch (item.EntityState)
                        {
                            case EntityState.New:
                                db.Sessions.InsertOnSubmit(item);
                                break;
                            case EntityState.Removed:
                                db.Sessions.DeleteOnSubmit(item);
                                break;
                            case EntityState.Modified:
                                db.Sessions.Attach(item, true);
                                break;
                        }
                    }

                    db.SubmitChanges();
                }
            }
        }

        #endregion

        #region Implementation of ISessionRepository

        /// <summary>
        /// Return the list of upcoming sessions from the repository
        /// </summary>
        public IList<Session> GetUpcomingSessions()
        {
            return Repository.Take(10).ToList();
        }

        /// <summary>
        /// Return a listing of all of the sessions in the repository
        /// </summary>
        /// <returns></returns>
        public IList<Session> GetAllSessions()
        {
            return Repository.OrderBy(s => s.Title).ToList();
        }

        /// <summary>
        /// Return sessions which contain the term
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public IList<Session> FindSessions(string searchTerm)
        {
            return Repository.Where(s => s.Title.ToLower().Contains(searchTerm.ToLower())).ToList();
        }

        #endregion

        #region Implementation of IRepository<Session>

        /// <summary>
        /// Get an item from the Repository based on its int key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Session Get(int id)
        {
            return Repository.First(s => s.SessionId == id);
        }

        #endregion
    }
}
