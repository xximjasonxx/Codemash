using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Provider.Impl;
using Ninject.Modules;

namespace Codemash.Api.Data.Modules
{
    public class DataProviderNinjectModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IMasterDataProvider>().To<DropboxMasterDataProvider>().InSingletonScope();
        }

        #endregion
    }
}
