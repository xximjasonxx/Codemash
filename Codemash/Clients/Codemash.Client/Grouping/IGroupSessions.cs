using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codemash.Client.Code;
using Codemash.Client.DataModels;

namespace Codemash.Client.Grouping
{
    public interface IGroupSessions
    {
        /// <summary>
        /// Returns the group list as dictated by the underlying implementation
        /// </summary>
        /// <returns></returns>
        IList<IListItem> GetGroupedList();
    }
}
