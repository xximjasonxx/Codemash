using Codemash.Phone.Data.Provider;
using Codemash.Phone.Data.Provider.Impl;
using Codemash.Phone.Data.Repository;
using Codemash.Phone.Data.Repository.Impl;
using Ninject.Modules;

namespace Codemash.Phone.Data.Modules
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
            //Bind<ISessionRepository>().To<JsonSessionRepository>().InSingletonScope();
            Bind<ISessionRepository>().To<TestSessionRepository>().InSingletonScope();
            //Bind<ISpeakerRepository>().To<JsonSpeakerRepository>().InSingletonScope();
            Bind<ISpeakerRepository>().To<TestSpeakerRepository>().InSingletonScope();
            //Bind<ISettingsRepository>().To<IsolatedStorageSettingsRepository>().InSingletonScope();
            Bind<ISettingsRepository>().To<TestSettingsRepository>().InSingletonScope();
            //Bind<IChangeRepository>().To<JsonChangeRepository>().InSingletonScope();
            Bind<IChangeRepository>().To<TestChangeRepository>().InSingletonScope();

            // bind providers
            Bind<IChangeProvider>().To<SessionSpeakerChangeProvider>();
            Bind<IChangeLogProvider>().To<DefaultChangeLogProvider>().InSingletonScope();
        }

        #endregion
    }
}
