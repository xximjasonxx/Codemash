using Codemash.Client.Code;

namespace Codemash.Client.Parameters
{
    public class GroupingParameter
    {
        public GroupingType GroupingType { get; private set; }

        public GroupingParameter(GroupingType groupingType)
        {
            GroupingType = groupingType;
        }
    }
}
