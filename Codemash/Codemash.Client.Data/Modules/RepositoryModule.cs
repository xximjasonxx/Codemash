using Codemash.Client.Data.Repository;
using Codemash.Client.Data.Repository.Impl;

namespace Codemash.Client.Data.Modules
{
    public class RepositoryModule : Ninject.Modules.NinjectModule
    {
        #region Overrides of NinjectModule

        public override void Load()
        {
            Bind<ISessionRepository>().To<JsonSessionRepository>().InSingletonScope().Named("Session");
        }

        #endregion
    }
}
