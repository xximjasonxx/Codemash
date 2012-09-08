using System;
using System.Collections.Generic;

namespace Codemash.Api.Data.Repositories
{
    public interface IWriteRepository<T, U>
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
