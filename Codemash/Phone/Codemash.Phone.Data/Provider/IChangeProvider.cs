using System;
using System.Collections.Generic;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Data.Provider
{
    public interface IChangeProvider
    {
        /// <summary>
        /// Apply changes
        /// </summary>
        /// <param name="changeList"></param>
        void ApplyChanges(IList<Change> changeList);
    }
}
