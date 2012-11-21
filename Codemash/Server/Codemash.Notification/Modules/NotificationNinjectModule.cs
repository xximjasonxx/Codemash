using Codemash.Notification.Factory;
using Codemash.Notification.Factory.Impl;
using Ninject.Modules;

namespace Codemash.Notification.Modules
{
    public class NotificationNinjectModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<INotificationFactory>().To<NotificationFactory>();
        }

        #endregion
    }
}
