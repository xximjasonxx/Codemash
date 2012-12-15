using System.Collections.Generic;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.Common.Services
{
    public interface IChangeService
    {
        /// <summary>
        /// Apply the Changes listed in the Change List
        /// </summary>
        /// <param name="changeList"></param>
        void ApplyChanges(IEnumerable<Change> changeList);
    }
}
