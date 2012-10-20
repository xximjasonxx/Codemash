using Codemash.Phone7.Data.Common;

namespace Codemash.Phone7.Data.Entities
{
    public abstract class EntityBase
    {
        internal EntityState EntityState { get; private set; }
        internal bool IsDirty { get { return EntityState != EntityState.Clean; } }
        internal abstract int PrimaryKey { get; }

        protected EntityBase()
        {
            EntityState = EntityState.New;
        }

        internal void MarkAsClean()
        {
            EntityState = EntityState.Clean;
        }
    }
}
