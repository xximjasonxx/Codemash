using Codemash.Phone.Data;
using Codemash.Phone.Shared.Common;
using Microsoft.Phone.Controls;

namespace Codemash.Phone7.App.Core
{
    public class Phone7CodemashContainer : CodemashContainer
    {
        public Phone7CodemashContainer(PhoneApplicationFrame frame) : base(frame)
        {
        }

        #region Overrides of CodemashContainer

        protected override void BindVersionSpecificDependencies(PhoneApplicationFrame frame)
        {
            Bind<ITileService>().To<Phone7TileService>();
        }

        #endregion
    }
}
