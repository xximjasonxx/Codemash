using System.Collections.Generic;
using Codemash.Phone.Data.Provider;

namespace Codemash.Phone.Shared.Common
{
    public class ProviderLogEntryDistinctComparer : IEqualityComparer<ProviderLogEntry>
    {
        public bool Equals(ProviderLogEntry x, ProviderLogEntry y)
        {
            return x.EntityId == y.EntityId;
        }

        public int GetHashCode(ProviderLogEntry obj)
        {
            return (int) obj.EntityId;
        }
    }
}
