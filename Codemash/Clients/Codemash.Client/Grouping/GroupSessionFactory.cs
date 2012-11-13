using System;
using Codemash.Client.Code;

namespace Codemash.Client.Grouping
{
    public static class GroupSessionFactory
    {
        public static IGroupSessions GetSessionGrouperInstance(GroupingType groupingType)
        {
            switch (groupingType)
            {
                case GroupingType.Alphabetical:
                    return new AlphanumericSessionGrouper();
                case GroupingType.Track:
                    return new TrackSessionGrouper();
                case GroupingType.Block:
                    return new BlockSessionGrouper();
                case GroupingType.Room:
                    return new RoomSessionGrouper();
                default:
                    throw new InvalidOperationException("Invalid Grouping Type");
            }
        }
    }
}
