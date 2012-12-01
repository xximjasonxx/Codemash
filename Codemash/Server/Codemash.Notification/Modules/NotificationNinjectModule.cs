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
            Bind<INotificationManagerResolver>().ToConstant(new NotificationManagerResolver(_container));

            Bind<INotificationManager>().To<WinPhone7NotificationManager>().Named("WinPhone7");
            Bind<INotificationManager>().To<WinPhone8NotificationManager>().Named("WinPhone8");
        }

        #endregion
    }
}
