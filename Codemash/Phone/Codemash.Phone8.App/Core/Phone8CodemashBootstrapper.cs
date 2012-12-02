using Codemash.Phone.Shared.Common;

namespace Codemash.Phone8.App.Core
{
    public class Phone8CodemashBootstrapper : CodemashBootstrapper
    {
        protected override void Configure()
        {
            NinjectContainer = new Phone8CodemashContainer(RootFrame);
        }
    }
}
