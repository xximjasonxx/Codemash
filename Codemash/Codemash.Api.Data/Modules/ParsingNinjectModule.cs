using Codemash.Api.Data.Parsing;
using Codemash.Api.Data.Parsing.Impl;
using Ninject.Modules;

namespace Codemash.Api.Data.Modules
{
    public class ParsingNinjectModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ISessionEntityParser>().To<CodemashSessionEntityParser>().InSingletonScope();
            Bind<ISpeakerEntityParser>().To<CodemashSpeakerEntityParser>().InSingletonScope();
        }

        #endregion
    }
}
