using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Codemash.Server.Core.Extensions;

namespace Codemash.Api.Data.Compare
{
    public class SessionCompare
    {
        /// <summary>
        /// Compare the Master and Local Session Lists
        /// </summary>
        /// <param name="masterList">The list is considered to contain the most up to date session information</param>
        /// <param name="localList">The list which is considered to contain the old session information</param>
        /// <returns></returns>
        public IList<SessionChange> CompareSessionLists(IList<Session> masterList, IList<Session> localList)
        {
            var changes = new List<SessionChange>();

            // add the changes resulting from a session being removed from local
            changes.AddRange(GetRemovedSessionChanges(masterList, localList));

            // add the changes resulting from a session being modified in the master source
            changes.AddRange(GetModifiedSessionChanges(masterList, localList));

            // add the changes resulting from a session being added to the master source
            changes.AddRange(GetAddedSessionChanges(masterList, localList));

            return changes;
        }

        /// <summary>
        /// Get the changes made which result in sessions being removed from the Local listing
        /// </summary>
        /// <param name="master">The master list as obtained from the master source</param>
        /// <param name="local">The local list which represents the local source</param>
        /// <returns></returns>
        private IEnumerable<SessionChange> GetRemovedSessionChanges(IEnumerable<Session> master, IEnumerable<Session> local)
        {
            var masterSessionIds = master.Select(s => s.SessionId).ToArray();
            var localSessionIds = local.Select(s => s.SessionId).ToArray();

            // determine which sessions are to be removed
            var removedSessions = localSessionIds.Where(id => !masterSessionIds.Contains(id));
            return removedSessions.Select(id => new SessionChange
                                                    {
                                                        SessionId = id,
                                                        Action = SessionChangeAction.Delete
                                                    });
        }

        /// <summary>
        /// Get the changes made which result in sessions being modified locally based on the master source
        /// </summary>
        /// <param name="masterList">Session data from the master source</param>
        /// <param name="localList">Local session data being persisted</param>
        /// <returns></returns>
        private IEnumerable<SessionChange> GetModifiedSessionChanges(IEnumerable<Session> masterList, IList<Session> localList)
        {
            var changes = new List<SessionChange>();
            foreach (var masterSession in masterList)
            {
                // attempt to find the master session locally
                var localSession = localList.FirstOrDefault(s => s.SessionId == masterSession.SessionId);
                if (localSession != null)
                {
                    var differences = masterSession.CompareTo(localSession);
                    if (differences.Count > 0)
                    {
                        changes.AddRange(CreateDifferencesList(differences, masterSession.SessionId));
                    }
                }
            }

            return changes;
        }

        /// <summary>
        /// Get the changes made which result in a session being added to the main source
        /// </summary>
        /// <param name="masterList">The session data from the master source</param>
        /// <param name="localList">The session data from the local source</param>
        /// <returns></returns>
        private IEnumerable<SessionChange> GetAddedSessionChanges(IEnumerable<Session> masterList, IEnumerable<Session> localList)
        {
            // find the IDs of sessions which are in the master but are not in local
            // add them to changes listing going back
            var localSessions = localList.Select(s => s.SessionId).ToArray();

            var changes = new List<SessionChange>();
            foreach (var session in masterList.Where(s => !localSessions.Contains(s.SessionId)))
            {
                changes.AddRange(GetDifferencesForAddedSession(session));
            }

            return changes;
        }

        /// <summary>
        /// Based on a Key Value pair of updated values resulting from the comparison of master and local create an
        /// IEnumerable holding those changes
        /// </summary>
        /// <param name="sessionDifferences">The differences between the sessions stored as key/value pairs</param>
        /// <param name="sessionId">The sessionId which has changed</param>
        /// <returns></returns>
        private IEnumerable<SessionChange> CreateDifferencesList(IEnumerable<KeyValuePair<string, string>> sessionDifferences, int sessionId)
        {
            return sessionDifferences.Select(kv => new SessionChange
                {
                    SessionId = sessionId, Action = SessionChangeAction.Modify, Key = kv.Key, Value = kv.Value
                }).ToList();
        }

        /// <summary>
        /// Create the difference record for the added session
        /// </summary>
        /// <param name="addedSession">The session which is being added to local via the master source</param>
        /// <returns></returns>
        private IEnumerable<SessionChange> GetDifferencesForAddedSession(Session addedSession)
        {
            // create a dummy session record
            var dummySession = Session.Dummy;
            var changes = addedSession.CompareTo(dummySession);
            return changes.Select(kv => new SessionChange
                                      {
                                          SessionId = addedSession.SessionId,
                                          Action = SessionChangeAction.Add,
                                          Key = kv.Key,
                                          Value = kv.Value
                                      });
        }
    }
}
