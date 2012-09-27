using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class EfSessionRepository : ISessionRepository
    {
        #region Implementation of ISessionRepository

        /// <summary>
        /// Get an item from the repository by a primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Session Get(int id)
        {
            return Get(s => s.SessionId == id);
        }

        /// <summary>
        /// Return the first instance of T which makes the given condition
        /// </summary>
        /// <param name="condition">The condition passed as a lambda predicate</param>
        /// <returns></returns>
        public Session Get(Func<Session, bool> condition)
        {
            using (var context = new CodemashContext())
            {
                return context.Sessions.FirstOrDefault(condition);
            }
        }

        /// <summary>
        /// Return all items in the Repository
        /// </summary>
        /// <returns></returns>
        public IList<Session> GetAll()
        {
            using (var context = new CodemashContext())
            {
                return context.Sessions.ToList();
            }
        }

        /// <summary>
        /// Return all items in the repository matching the given condition
        /// </summary>
        /// <param name="condition">A condition passed as a lambda predicate</param>
        /// <returns></returns>
        public IList<Session> GetAll(Func<Session, bool> condition)
        {
            using (var context = new CodemashContext())
            {
                return context.Sessions.Where(condition).ToList();
            }
        }

        #endregion

        #region Implementation of IWriteRepository<Session,int>

        /// <summary>
        /// Apply the changes (add/remove) from a Master Session List
        /// </summary>
        /// <param name="masterSessionList">The session data from the master source</param>
        /// <remarks>The incoming range is expected to be the full measure of session data</remarks>
        public void SaveRange(IEnumerable<Session> masterSessionList)
        {
            var masterList = masterSessionList.ToList();
            using (var context = new CodemashContext())
            {
                // get all available speakers
                var availabelSpeakers = context.Speakers.Select(sp => sp.SpeakerId).ToArray();

                // filter the master list so that sessions which do not have a valid speaker are not included
                masterList = masterList.Where(s => availabelSpeakers.Contains(s.SpeakerId)).ToList();

                SaveChanges(context, masterList);
                SaveRemovals(context, masterList);

                context.SaveChanges();
            }
        }

        #endregion

        /// <summary>
        /// Save modifications to the existing session data
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessions"></param>
        private static void SaveChanges(CodemashContext context, IEnumerable<Session> sessions)
        {
            var existingSessions = context.Sessions.ToList();
            foreach (var session in sessions.ToList())
            {
                var existingSession = existingSessions.FirstOrDefault(s => s.SessionId == session.SessionId);
                if (existingSession == null)
                {
                    context.Sessions.Add(session);
                    continue;
                }

                // copy the fields to the existing object
                existingSession.Title = session.Title;
                existingSession.Abstract = session.Abstract;
                existingSession.Start = session.Start;
                existingSession.End = session.End;
                existingSession.LevelType = session.LevelType;
                existingSession.RoomType = session.RoomType;
                existingSession.SpeakerId = session.SpeakerId;
                existingSession.TrackType = session.TrackType;
            }
        }

        /// <summary>
        /// Remove the sessions in the database which do not appear in the incoming session list
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessions"></param>
        private static void SaveRemovals(CodemashContext context, IList<Session> sessions)
        {
            // first check if the session has been totally removed
            var existingSessions = context.Sessions.Select(s => s.SessionId);
            var masterSessions = sessions.Select(s => s.SessionId);
            var sessionsToRemove = existingSessions.Where(i => !masterSessions.Contains(i)).ToList();

            // perform the mass update
            sessionsToRemove.ForEach(s => context.Sessions.Remove(context.Sessions.FirstOrDefault(se => se.SessionId == s)));
        }
    }
}
