using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Data
{
    internal enum EntityState
    {
        Deleted,
        Modified,
        New,
        Unmodified
    }

    public enum ActionType
    {
        Add,
        Modify,
        Delete
    }
}
