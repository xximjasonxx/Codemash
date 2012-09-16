using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface IWriteRepository<T, U> where T : EntityBase
    {
        /// <summary>
        /// Commit all changes in the repository
        /// </summary>
        void SaveRange(IEnumerable<T> entityList);
    }
}
