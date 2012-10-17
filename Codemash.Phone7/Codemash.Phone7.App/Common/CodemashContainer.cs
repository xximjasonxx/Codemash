using Caliburn.Micro;
using Codemash.Phone7.Data.Modules;
using Microsoft.Phone.Controls;
using Ninject;

namespace Codemash.Phone7.App.Common
{
    public class CodemashContainer : StandardKernel
    {
        public CodemashContainer(PhoneApplicationFrame frame)
        {
            // bind standard Caliburn Components
            Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            Bind<INavigationService>().ToConstant(new FrameAdapter(frame)).InSingletonScope();
            Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            Bind<IPhoneService>().ToConstant(new PhoneApplicationServiceAdapter(frame)).InSingletonScope();

            // bind repositories
            Load(new[] {new CodemashRepositoryModule()});
        }
    }
}
