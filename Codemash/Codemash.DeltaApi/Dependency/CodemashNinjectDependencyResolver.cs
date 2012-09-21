using System.Web.Http.Dependencies;
using Ninject;

namespace Codemash.DeltaApi.Dependency
{
    public class CodemashNinjectDependencyResolver : CodemashNinjectDependencyScope, IDependencyResolver
    {
        private readonly IKernel _theKernel;

        public CodemashNinjectDependencyResolver(IKernel kernel) : base(kernel)
        {
            _theKernel = kernel;
        }

        #region Implementation of IDependencyResolver

        /// <summary>
        /// Starts a resolution scope. 
        /// </summary>
        /// <returns>
        /// The dependency scope.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            return new CodemashNinjectDependencyScope(_theKernel.BeginBlock());
        }

        #endregion
    }
}