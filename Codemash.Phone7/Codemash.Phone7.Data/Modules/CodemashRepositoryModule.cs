using Codemash.Phone7.Data.Repository;
using Codemash.Phone7.Data.Repository.Impl;
using Ninject.Modules;

namespace Codemash.Phone7.Data.Modules
{
    public class CodemashRepositoryModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            // bind repositories
            Bind<ISessionRepository>().To<JsonSessionRepository>().InSingletonScope();
            Bind<ISpeakerRepository>().To<JsonSpeakerRepository>().InSingletonScope();
        }

        #endregion
    }
}
