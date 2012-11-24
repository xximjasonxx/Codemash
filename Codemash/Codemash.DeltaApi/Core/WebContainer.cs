using System.Reflection;
using Codemash.Api.Data.Modules;
using Codemash.DeltaApi.Modules;
using Codemash.Notification.Modules;
using Ninject;
using Ninject.Modules;

namespace Codemash.DeltaApi.Core
{
    public class WebContainer : StandardKernel
    {
        public WebContainer()
        {
            // load the other modules
            var controllerModule = new AssemblyIHttpControllerNinjectModule();
            var repositoryModule = new RepositoryNinjectModule();
            var notificationModule = new NotificationNinjectModule(this);

            // load em
            Load(new INinjectModule[] { controllerModule, repositoryModule, notificationModule });
        }
    }
}