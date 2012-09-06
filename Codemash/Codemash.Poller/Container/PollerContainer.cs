using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Provider.Impl;
using Codemash.Api.Data.Repositories;
using Codemash.Api.Data.Repositories.Impl;
using Codemash.Poller.Process;
using Ninject;

namespace Codemash.Poller.Container
{
    public class PollerContainer : StandardKernel
    {
        public PollerContainer()
        {
            // bind the worker process
            Bind<PollerWorkerProcess>().ToSelf().InSingletonScope();

            // bind the master data provider
            Bind<IMasterDataProvider>().To<SkydriveMasterDataProvider>().InSingletonScope();

            // bind the repositories
            Bind<ISessionRepository>().To<BlobSessionRepository>().InSingletonScope();
            Bind<ISpeakerRepository>().To<BlobSpeakerRepository>().InSingletonScope();
        }
    }
}
