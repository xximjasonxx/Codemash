
using Ninject;
using Ninject.Parameters;

namespace Codemash.Poller.Notification.Impl.Resolver
{
    public class PollerNotificationManagerResolver : INotificationManagerResolver
    {
        private readonly IKernel _container;

        public PollerNotificationManagerResolver(IKernel container)
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
