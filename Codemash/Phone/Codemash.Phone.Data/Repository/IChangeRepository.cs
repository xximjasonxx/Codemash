using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Data.Repository
{
    public interface IChangeRepository : IRepository<Change>
    {
        /// <summary>
        /// Return whether there are changes in the repository or not
        /// </summary>
        bool HasChanges { get; }
    }
}
