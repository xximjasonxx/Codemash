using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Server.Core.Attributes;
using Codemash.Server.Core.Extensions;

namespace Codemash.Api.Data.Extensions
{
    public static class ComparisonExtensionMethods
    {
        /// <summary>
        /// Compare two list of entities and return their differences for storage
        /// </summary>
        /// <typeparam name="T">The type of entities being compared</typeparam>
        /// <typeparam name="TChange">The type of change entities being returned which record all changes</typeparam>
        /// <param name="masterList">The list of the master data - the latest copy</param>
        /// <param name="localList">The list of the current data - this is what is current, not the latest</param>
        /// <returns></returns>
        public static IList<TChange> Compare<T, TChange>(this IEnumerable<T> masterList, IList<T> localList) where T : EntityBase, IHasIdentifier, new() where TChange : class, IChange, new()
        {
            var differences = new List<TChange>();

            // find items which were modified
            differences.AddRange(masterList.FindModifiedDifferencesWith<T, TChange>(localList));

            // find items which were removed
            differences.AddRange(masterList.FindRemovedDifferencesWith<T, TChange>(localList));

            // find items which were added
            differences.AddRange(masterList.FindAddedDifferencesWith<T, TChange>(localList));

            return differences;
        }

        #region Change Detection methods

        private static IList<TChange> FindModifiedDifferencesWith<T, TChange>(this IEnumerable<T> masterList, IList<T> localList) where T : EntityBase, IHasIdentifier where TChange : IChange, new()
        {
            var changes = new List<TChange>();
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
                        changes.AddRange(CreateDifferencesList<TChange>(differences, local.ID));
                    }
                }
            }

            return changes;
        }

        private static IList<TChange> FindRemovedDifferencesWith<T, TChange>(this IEnumerable<T> masterList, IEnumerable<T> localList) where T : IHasIdentifier where TChange : IChange, new()
        {
            var masterIds = masterList.Select(s => s.ID).ToArray();
            var localIds = localList.Select(s => s.ID).ToArray();

            // determine which sessions are to be removed
            var removedSessions = localIds.Where(id => !masterIds.Contains(id));
            return removedSessions.Select(id => new TChange
                                                    {
                                                        ID = id,
                                                        Action = ChangeAction.Delete
                                                    }).ToList();
        }

        private static IList<TChange> FindAddedDifferencesWith<T, TChange>(this IEnumerable<T> masterList, IEnumerable<T> localList) where T : EntityBase, IHasIdentifier, new() where TChange : class, IChange, new() 
        {
            // find the IDs of sessions which are in the master but are not in local
            // add them to changes listing going back
            var localSessions = localList.Select(s => s.ID).ToArray();

            var changes = new List<TChange>();
            foreach (var entity in masterList.Where(s => !localSessions.Contains(s.ID)))
            {
                var newEntity = new T();
                var differences = CreateDifferencesList<TChange>(entity.CompareTo(newEntity), entity.ID).ToList();

                // by defailt CreateDifferencesList will mark the action as modify - we need to override this
                differences.Apply(d => d.Action = ChangeAction.Add);

                changes.AddRange(differences);
            }

            return changes;
        }

        #endregion

        /// <summary>
        /// Analyze a Sesssion object and return a key value pair with Property/Value differences
        /// </summary>
        /// <param name="first">The session being extended (new)</param>
        /// <param name="second">The session being compared against (old)</param>
        /// <returns></returns>
        public static IDictionary<string, string> CompareTo<T>(this T first, T second) where T : EntityBase
        {
            // begin looping through the values of thisSession
            // only properties marked for comparison
            var masterProperties = first.GetType().GetProperties()
                .Where(p => p.GetCustomAttributes(false).OfType<ComparableAttribute>().Any())
                .ToList();

            var returnResults = new Dictionary<string, string>();
            foreach (var property in masterProperties)
            {
                var masterValue = property.GetValue(first, null).ToString();
                var childValue = second.GetType().GetProperty(property.Name).GetValue(second, null).ToString();

                if (masterValue.CompareTo(childValue) != 0)
                {
                    returnResults.Add(property.Name, masterValue);
                }
            }

            return returnResults;
        }

        /// <summary>
        /// Based on a Key Value pair of updated values resulting from the comparison of master and local create an
        /// IEnumerable holding those changes
        /// </summary>
        /// <param name="differences">The differences between the entities stored as key/value pairs</param>
        /// <param name="id">The id which has changed</param>
        /// <returns></returns>
        private static IEnumerable<TChange> CreateDifferencesList<TChange>(IEnumerable<KeyValuePair<string, string>> differences, int id) where TChange : IChange, new()
        {
            return differences.Select(kv => new TChange
            {
                ID = id,
                Action = ChangeAction.Modify,
                Key = kv.Key,
                Value = kv.Value
            }).ToList();
        }
    }
}
