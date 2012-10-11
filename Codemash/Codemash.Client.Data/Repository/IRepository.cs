using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.Data.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Load the repository with data
        /// </summary>
        void Load();

        /// <summary>
        /// Indicates Load has completed
        /// </summary>
        event EventHandler LoadCompleted;
    }
}
