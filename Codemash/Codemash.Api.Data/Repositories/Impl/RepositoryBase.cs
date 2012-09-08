using System.Collections.Generic;
using Codemash.Api.Data.Ex;

namespace Codemash.Api.Data.Repositories.Impl
{
    public abstract class RepositoryBase<T> : List<T>
    {
        private bool RepositoryLoaded { get; set; }

        /// <summary>
        /// Reset the internal flag responsible for indicating whether the repository has called Load or not
        /// </summary>
        protected void ResetHasLoadedFlag()
        {
            RepositoryLoaded = false;
        }

        /// <summary>
        /// Indicate that Load has been called and the Repository is ready to receive external access
        /// </summary>
        protected void RepositoryHasLoaded()
        {
            RepositoryLoaded = true;
        }

        /// <summary>
        /// Check the current state to determine if external usage is allowed - throw an exception if it is not
        /// </summary>
        protected void CheckRepositoryHasLoaded()
        {
            if (!RepositoryLoaded && Count == 0)
                throw new RepositoryNotLoadedException(RepositoryName);
        }

        /// <summary>
        /// Name of the repository
        /// </summary>
        public abstract string RepositoryName { get; }
    }
}
