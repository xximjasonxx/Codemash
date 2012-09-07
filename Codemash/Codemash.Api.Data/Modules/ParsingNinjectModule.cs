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
            Bind<RoomParse>().ToSelf().InSingletonScope();
            Bind<TrackParse>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}
