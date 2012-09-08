using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface IWriteRepository<T, U> where T : EntityBase
    {
        /// <summary>
        /// Add items to the Repository
        /// </summary>
        /// <param name="items">The items to be added to the repository</param>
        void AddRange(IEnumerable<T> items);

        /// <summary>
        /// Commit all changes in the repository
        /// </summary>
        void Save();
    }
}
