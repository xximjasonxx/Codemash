using Codemash.Phone.Shared.Common;

namespace Codemash.Phone7.App.Core
{
    public class Phone7CodemashBootstrapper : CodemashBootstrapper
    {
        protected override void Configure()
        {
            NinjectContainer = new Phone7CodemashContainer(RootFrame);
        }
    }
}
