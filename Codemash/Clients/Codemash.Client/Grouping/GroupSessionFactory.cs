using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codemash.Client.Code;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.Grouping
{
    public static class GroupSessionFactory
    {
        public static IGroupSessions GetSessionGrouperInstance(IList<Session> sessionList, GroupingType groupingType)
        {
            switch (groupingType)
            {
                case GroupingType.Alphabetical:
                    return new AlphanumericSessionGrouper(sessionList);
                case GroupingType.Track:
                    return new TrackSessionGrouper(sessionList);
                case GroupingType.Block:
                    return new BlockSessionGrouper(sessionList);
                default:
                    throw new InvalidOperationException("Invalid Grouping Type");
            }
        }
    }
}
