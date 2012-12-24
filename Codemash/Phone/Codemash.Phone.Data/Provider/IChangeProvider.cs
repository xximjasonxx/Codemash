using System.Collections.Generic;
using Codemash.Phone.Data.Common;

namespace Codemash.Phone.Data.Provider
{
    public interface IChangeProvider
    {
        /// <summary>
        /// Apply changes
        /// </summary>
        /// <param name="changeList"></param>
        void ApplyChanges(ChangeList changeList);
    }
}
