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
            get { return EntityState != EntityState.Unmodified; }
        }

        protected void MarkDirty()
        {
            EntityState = EntityState.Modified;
        }
    }
}
