using Codemash.Phone.Data;
using Codemash.Phone.Shared.Common;
using Microsoft.Phone.Controls;

namespace Codemash.Phone8.App.Core
{
    public class Phone8CodemashContainer : CodemashContainer
    {
        public Phone8CodemashContainer(PhoneApplicationFrame frame) : base(frame)
        {
        }

        #region Overrides of CodemashContainer

        protected override void BindVersionSpecificDependencies(PhoneApplicationFrame frame)
        {
            Bind<ITileService>().To<Phone8TileService>();
        }

        #endregion
    }
}
