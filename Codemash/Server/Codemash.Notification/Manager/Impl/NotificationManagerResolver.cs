using Ninject;
using Ninject.Parameters;

namespace Codemash.Notification.Manager.Impl
{
    public class NotificationManagerResolver : INotificationManagerResolver
    {
        private readonly IKernel _container;

        public NotificationManagerResolver(IKernel container)
        {
            _container = container;
        }

        #region Implementation of IResolver<INotificationManager>

        /// <summary>
        /// Resolve to an instance of type T
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public INotificationManager Resolve(string key)
        {
            return _container.Get<INotificationManager>(key, new IParameter[0]);
        }

        #endregion
    }
}
