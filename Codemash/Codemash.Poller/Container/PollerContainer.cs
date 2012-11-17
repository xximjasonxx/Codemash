using System.Collections.Generic;
using Codemash.Api.Data.Modules;
using Codemash.Poller.Notification.Module;
using Codemash.Poller.Process;
using Ninject;
using Ninject.Modules;

namespace Codemash.Poller.Container
{
    public class PollerContainer : StandardKernel
    {
        public PollerContainer()
        {
            // bind the worker process
            Bind<ISynchronize>().To<SessionSynchronize>().InSingletonScope().Named("Session");
            Bind<ISynchronize>().To<SpeakerSynchronize>().InSingletonScope().Named("Speaker");
            Bind<IProcess>().To<NotificationCheckProcess>().InSingletonScope();

            // define all external modules
            var dataProviderModule = new DataProviderNinjectModule();
            var repositoryModule = new RepositoryNinjectModule();
            var parsingModule = new ParsingNinjectModule();
            var notificationModule = new PollerNotificationNinjectModule(this);

            // add them into container
            Load(new List<INinjectModule>
                {
                    dataProviderModule,
                    repositoryModule,
                    parsingModule,
                    notificationModule
                });
        }
    }
}
