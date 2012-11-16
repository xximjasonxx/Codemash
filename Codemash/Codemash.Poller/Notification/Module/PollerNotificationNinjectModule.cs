using Codemash.Api.Data;
using Codemash.Poller.Notification.Impl.Manager;
using Codemash.Poller.Notification.Impl.Resolver;
using Ninject;
using Ninject.Modules;

namespace Codemash.Poller.Notification.Module
{
    public class PollerNotificationNinjectModule : NinjectModule
    {
        private readonly IKernel _container;

        public PollerNotificationNinjectModule(IKernel container)
        {
            _container = container;
        }

        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            // bind notification managers
            Bind<INotificationManager>().To<Win8NotificationManager>().InSingletonScope().Named(
                PushClientTypes.Win8.ToString());

            Bind<INotificationManager>().To<WinPhone8NotificationManager>().InSingletonScope().Named(
                PushClientTypes.WinPhone8.ToString());

            Bind<INotificationManager>().To<WinPhone7NotificationManager>().InSingletonScope().Named(
                PushClientTypes.WinPhone7.ToString());

            // bind the resolver
            var resolver = new PollerNotificationManagerResolver(_container);
            Bind<INotificationManagerResolver>().ToConstant(resolver).InSingletonScope();
        }

        #endregion
    }
}
