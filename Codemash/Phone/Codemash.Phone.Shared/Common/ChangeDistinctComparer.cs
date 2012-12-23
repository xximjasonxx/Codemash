using System.Collections.Generic;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Shared.Common
{
    public class ChangeDistinctComparer : IEqualityComparer<Change>
    {
        public bool Equals(Change x, Change y)
        {
            return x.EntityId == y.EntityId;
        }

        public int GetHashCode(Change obj)
        {
            return (int) obj.EntityId;
        }
    }
}
