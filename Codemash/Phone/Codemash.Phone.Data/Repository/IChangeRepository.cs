using System.Collections.Generic;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Data.Repository
{
    public interface IChangeRepository : IRepository<Change>
    {
        /// <summary>
        /// Return all changes in the current repository
        /// </summary>
        /// <returns></returns>
        IList<Change> GetAll();
    }
}
