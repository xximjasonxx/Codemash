using Codemash.Notification.Helper;
using Ninject;
using Ninject.Parameters;

namespace Codemash.Notification
{
    public class NotificationHelperResolver : INotificationHelperResolver
    {
        private readonly IKernel _container;

        public NotificationHelperResolver(IKernel container)
        {
            _container = container;
        }

        #region Implementation of IResolver<INotificationHelper>

        /// <summary>
        /// Resolve to an instance of type T
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public INotificationHelper Resolve(string key)
        {
            return _container.Get<INotificationHelper>(key, new IParameter[0]);
        }

        #endregion
    }
}
