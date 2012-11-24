using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Phone.Core;
using Codemash.Phone.Data.Common;
using Codemash.Phone.Data.Entities;
using Newtonsoft.Json.Linq;

namespace Codemash.Phone.Data.Repository.Impl
{
    public class JsonSessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        #region Overrides of RepositoryBase<Session>

        protected override string DownloadUrl
        {
            get { return "http://192.168.1.4/DeltaApi/api/Session"; }
        }

        protected override Session CreateObject(JToken jToken)
        {
            var session = new Session()
                              {
                                  Abstract = new StringWrapper(jToken["Abstract"]).ToString(),
                                  Difficulty = new StringWrapper(jToken["Level"]).ToString(),
                                  SessionId = new StringWrapper(jToken["SessionId"]).ToString().AsLong(),
                                  SpeakerId = new StringWrapper(jToken["SpeakerId"]).ToString().AsLong(),
                                  Technology = new StringWrapper(jToken["Track"]).ToString(),
                                  Title = new StringWrapper(jToken["Title"]).ToString(),
                                  Starts = new StringWrapper(jToken["Start"]).ToString(),
                                  Ends = new StringWrapper(jToken["End"]).ToString(),
                                  Room = new StringWrapper(jToken["Room"]).ToString()
                              };

            return session;
        }

        /// <summary>
        /// Instructs the repository to Save all dirty records
        /// </summary>
        protected override void Save()
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
            var blocks = Repository.Select(s => s.Starts.AsDateTime()).Distinct().ToList();
            if (blocks.Count(d => d >= DateTime.Now) == 0)
            {
                return new List<Session>();
            }

            var upcomingBlock = blocks.OrderBy(d => d).FirstOrDefault(d => d >= DateTime.Now);
            return Repository.Where(s => s.Starts.AsDateTime() == upcomingBlock).OrderBy(s => s.Title).ToList();
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
        public Session Get(long id)
        {
            return Repository.First(s => s.SessionId == id);
        }

        #endregion
    }
}
