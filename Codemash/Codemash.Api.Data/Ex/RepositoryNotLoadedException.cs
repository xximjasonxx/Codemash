using System;

namespace Codemash.Api.Data.Ex
{
    public class RepositoryNotLoadedException : Exception
    {
        public RepositoryNotLoadedException(string repositoryName) :
            base(repositoryName + " Repository not loaded")
        {
            // nothing
        }
    }
}
