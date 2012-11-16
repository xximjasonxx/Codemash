﻿using Caliburn.Micro;
using Codemash.Phone.Data.Modules;
using Codemash.Phone.Shared.Services;
using Codemash.Phone.Shared.Services.Impl;
using Microsoft.Phone.Controls;
using Ninject;

namespace Codemash.Phone.Shared.Common
{
    public class CodemashContainer : StandardKernel
    {
        public CodemashContainer(PhoneApplicationFrame frame, PhoneClientType clientType)
        {
            // bind standard Caliburn Components
            Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            Bind<INavigationService>().ToConstant(new FrameAdapter(frame)).InSingletonScope();
            Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            Bind<IPhoneService>().ToConstant(new PhoneApplicationServiceAdapter(frame)).InSingletonScope();

            // bind repositories
            Load(new[] {new CodemashRepositoryModule()});

            // bind custom services
            var appService = new CustomAppService(clientType);
            Bind<IAppService>().ToConstant(appService).InSingletonScope();
        }
    }
}
