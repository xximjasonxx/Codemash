using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Data.Entities
{
    public abstract class EntityBase
    {
        internal EntityState EntityState { get; private set; }

        protected EntityBase()
        {
            EntityState = EntityState.New;
        }

        internal void MarkUnmodified()
        {
            EntityState = EntityState.Unmodified;
        }

        internal bool IsDirty
        {
            get
            {
                var dirty = false;
                dirty |= EntityState == EntityState.Deleted;
                dirty |= EntityState == EntityState.Modified;
                dirty |= EntityState == EntityState.New;

                return dirty;
            }
        }
    }
}
