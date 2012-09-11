using System;
using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface IReadRepository<T, U> where T : EntityBase
    {
        /// <summary>
        /// Get an item from the repository by a primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(U id);

        /// <summary>
        /// Return the first instance of T which makes the given condition
        /// </summary>
        /// <param name="condition">The condition passed as a lambda predicate</param>
        /// <returns></returns>
        T Get(Func<T, bool> condition);

        /// <summary>
        /// Return all items in the Repository
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        /// <summary>
        /// Return all items in the repository matching the given condition
        /// </summary>
        /// <param name="condition">A condition passed as a lambda predicate</param>
        /// <returns></returns>
        IList<T> GetAll(Func<T, bool> condition);

        /// <summary>
        /// Empty the repository without saving any values
        /// </summary>
        void Clear();
    }
}
