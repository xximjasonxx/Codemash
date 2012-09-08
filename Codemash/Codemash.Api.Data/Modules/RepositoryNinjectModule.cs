using Codemash.Api.Data.Repositories;
using Codemash.Api.Data.Repositories.Impl;
using Ninject.Modules;

namespace Codemash.Api.Data.Modules
{
    public class RepositoryNinjectModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ISessionRepository>().To<BlobSessionRepository>().InThreadScope();
            Bind<ISpeakerRepository>().To<BlobSpeakerRepository>().InThreadScope();
            Bind<ISessionChangeRepository>().To<BlobSessionChangeRepository>().InThreadScope();
        }

        #endregion
    }
}
