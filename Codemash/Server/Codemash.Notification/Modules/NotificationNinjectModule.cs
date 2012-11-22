using Codemash.Notification.Factory;
using Codemash.Notification.Factory.Impl;
using Codemash.Notification.Helper;
using Codemash.Notification.Helper.Impl;
using Codemash.Notification.Manager;
using Codemash.Notification.Manager.Impl;
using Ninject;
using Ninject.Modules;

namespace Codemash.Notification.Modules
{
    public class NotificationNinjectModule : NinjectModule
    {
        private readonly IKernel _container;

        public NotificationNinjectModule(IKernel container)
        {
            _container = container;
        }

        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<INotificationFactory>().To<NotificationFactory>();
            Bind<INotificationHelperResolver>().ToConstant(new NotificationHelperResolver(_container));
            Bind<INotificationManagerResolver>().ToConstant(new NotificationManagerResolver(_container));

            Bind<INotificationHelper>().To<WinPhone7NotificationHelper>().Named("WinPhone7");

            Bind<INotificationManager>().To<WinPhone7NotificationManager>().Named("WinPhone7");
        }

        #endregion
    }
}
