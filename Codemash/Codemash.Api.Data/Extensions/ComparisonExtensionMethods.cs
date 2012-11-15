using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Entities.Interfaces;
using Codemash.Server.Core.Extensions;

namespace Codemash.Api.Data.Extensions
{
    public static class ComparisonExtensionMethods
    {
        /// <summary>
        /// Compare two list of entities and return their differences for storage
        /// </summary>
        /// <typeparam name="T">The type of entities being compared</typeparam>
        /// <param name="masterList">The list of the master data - the latest copy</param>
        /// <param name="localList">The list of the current data - this is what is current, not the latest</param>
        /// <returns></returns>
        public static IList<Change> Compare<T>(this IList<T> masterList, IList<T> localList) where T : EntityBase, IHasIdentifier, new()
        {
            var differences = new List<Change>();

            // find items which were modified
            differences.AddRange(masterList.FindModifiedDifferencesWith(localList));

            // find items which were removed
            differences.AddRange(masterList.FindRemovedDifferencesWith(localList));

            // find items which were added
            differences.AddRange(masterList.FindAddedDifferencesWith(localList));

            return differences;
        }

        #region Change Detection methods

        private static IEnumerable<Change> FindModifiedDifferencesWith<T>(this IEnumerable<T> masterList, IList<T> localList) where T : EntityBase, IHasIdentifier
        {
            var changes = new List<Change>();
            foreach (var master in masterList)
            {
                // attempt to find the master session locally
                var local = localList.FirstOrDefault(s => s.ID == master.ID);
                if (local != null)
                {
                    var differences = master.CompareTo(local);
                    if (differences.Count > 0)
                    {
                        // add things
                        changes.AddRange(CreateDifferencesList<T>(differences, local.ID));
                    }
                }
            }

            return changes;
        }

        private static IEnumerable<Change> FindRemovedDifferencesWith<T>(this IEnumerable<T> masterList, IEnumerable<T> localList) where T : IHasIdentifier
        {
            var masterIds = masterList.Select(s => s.ID).ToArray();
            var localIds = localList.Select(s => s.ID).ToArray();

            // determine which sessions are to be removed
            var removals = localIds.Where(id => !masterIds.Contains(id));
            return removals.Select(id => new Change
                                                    {
                                                        EntityId = id,
                                                        EntityType = typeof(T).Name,
                                                        Action = ChangeAction.Delete.ToString(),
                                                        Key = string.Empty,
                                                        Value = string.Empty
                                                    }).ToList();
        }

        private static IEnumerable<Change> FindAddedDifferencesWith<T>(this IEnumerable<T> masterList, IEnumerable<T> localList) where T : EntityBase, IHasIdentifier, new()
        {
            // find the IDs of sessions which are in the master but are not in local
            // add them to changes listing going back
            var locals = localList.Select(s => s.ID).ToArray();

            var changes = new List<Change>();
            foreach (var entity in masterList.Where(s => !locals.Contains(s.ID)))
            {
                var newEntity = new T();
                var differences = CreateDifferencesList<T>(entity.CompareTo(newEntity), entity.ID).ToList();

                // by defailt CreateDifferencesList will mark the action as modify - we need to override this
                differences.Apply(d => d.Action = ChangeAction.Add.ToString());

                changes.AddRange(differences);
            }

            return changes;
        }

        #endregion

        /// <summary>
        /// Based on a Key Value pair of updated values resulting from the comparison of master and local create an
        /// IEnumerable holding those changes
        /// </summary>
        /// <param name="differences">The differences between the entities stored as key/value pairs</param>
        /// <param name="id">The id which has changed</param>
        /// <returns></returns>
        private static IEnumerable<Change> CreateDifferencesList<T>(IEnumerable<KeyValuePair<string, string>> differences, long id)
        {
            return differences.Select(kv => new Change
            {
                EntityId = id,
                Action = ChangeAction.Modify.ToString(),
                EntityType = typeof(T).Name,
                Key = kv.Key,
                Value = kv.Value
            }).ToList();
        }
    }
}
