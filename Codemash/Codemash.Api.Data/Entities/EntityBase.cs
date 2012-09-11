
namespace Codemash.Api.Data.Entities
{
    public abstract class EntityBase
    {
        /// <summary>
        /// The current state of the entity
        /// </summary>
        public EntityState CurrentState { get; private set; }

        /// <summary>
        /// Has the entity been modified in anyway
        /// </summary>
        public bool IsDirty
        {
            get { return CurrentState != EntityState.Unmodified; }
        }

        /// <summary>
        /// When the constructor is invoked the entity is assumed to be New
        /// </summary>
        protected EntityBase()
        {
            CurrentState = EntityState.New;
        }

        /// <summary>
        /// Indicate that this is a known existing entity and that it should override whatever CurrentState is
        /// </summary>
        public void MarkAsExisting()
        {
            CurrentState = EntityState.Unmodified;
        }

        /// <summary>
        /// Mark the given entity as removed - the save operation will delete it
        /// </summary>
        public void MarkRemoved()
        {
            CurrentState = EntityState.Removed;
        }

        /// <summary>
        /// Indicate that a property on the Entity has changed
        /// </summary>
        protected void ValueChanged()
        {
            if (CurrentState != EntityState.New)
                CurrentState = EntityState.Modified;
        }
    }
}
