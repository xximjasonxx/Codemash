using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using Codemash.DeltaApi.Core;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;

namespace Codemash.DeltaApi.Dependency
{
    public class CodemashNinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot _resolutionRoot;

        public CodemashNinjectDependencyScope(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            IDisposable disposable = _resolutionRoot as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            _resolutionRoot = null;
        }

        #endregion

        #region Implementation of IDependencyScope

        /// <summary>
        /// Retrieves a service from the scope.
        /// </summary>
        /// <returns>
        /// The retrieved service.
        /// </returns>
        /// <param name="serviceType">The service to be retrieved.</param>
        public object GetService(Type serviceType)
        {
            string controllerName = serviceType.Name.AsControllerName();
            if (string.IsNullOrEmpty(controllerName))
                return null;

            return _resolutionRoot.TryGet<IHttpController>(controllerName, new IParameter[0]);
        }

        /// <summary>
        /// Retrieves a collection of services from the scope.
        /// </summary>
        /// <returns>
        /// The retrieved collection of services.
        /// </returns>
        /// <param name="serviceType">The collection of services to be retrieved.</param>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}