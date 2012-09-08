using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;

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
    }
}
