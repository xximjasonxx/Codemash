using Codemash.Phone.Data.Common;

namespace Codemash.Phone.Data.Entities
{
    public abstract class EntityBase
    {
        internal EntityState EntityState { get; private set; }
        internal bool IsDirty { get { return EntityState != EntityState.Clean; } }

        protected EntityBase()
        {
            EntityState = EntityState.New;
        }

        internal void MarkAsClean()
        {
            EntityState = EntityState.Clean;
        }

        internal void MarkAsDeleted()
        {
            EntityState = EntityState.Removed;
        }

        protected void MarkAsDirty()
        {
            if (EntityState == EntityState.New)
                return;

            EntityState = EntityState.Modified;
        }
    }
}
