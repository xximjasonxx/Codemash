using System;
using Codemash.Phone7.App.Common;

namespace Codemash.Phone7.App.Grouping
{
    public static class GroupingFactory
    {
        public static IGroup GetGroupInstance(SessionGroupType groupType)
        {
            switch (groupType)
            {
                case SessionGroupType.ByTech:
                    return new ByTechGroup();
                case SessionGroupType.ByBlock:
                    return new ByBlockGroup();
                case SessionGroupType.ByName:
                    return new ByNameGroup();
            }

            throw new InvalidOperationException("Unable to determine grouping type");
        }
    }
}
