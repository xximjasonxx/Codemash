using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Api.Data.Entities
{
    public abstract class EntityBase
    {
        public bool IsDirty { get; protected set; }
        public bool IsNew { get; protected set; }

        // methods
        internal bool IsExisting
        {
            set { IsNew = !value; }
        }
    }
}
