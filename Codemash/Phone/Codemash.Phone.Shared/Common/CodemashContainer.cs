using Caliburn.Micro;
using Codemash.Phone.Data.Modules;
using Codemash.Phone.Shared.Services;
using Codemash.Phone.Shared.Services.Impl;
using Microsoft.Phone.Controls;
using Ninject;

namespace Codemash.Phone.Shared.Common
{
    public abstract class CodemashContainer : StandardKernel
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

            // bind custom services
            Bind<IAppService>().To<CustomAppService>().InSingletonScope();
            Bind<INotificationRegistrationService>().To<CustomNotificationRegistrationService>();

            // bind version specific dependencies
            BindVersionSpecificDependencies(frame);
        }

        protected abstract void BindVersionSpecificDependencies(PhoneApplicationFrame frame);
    }
}
