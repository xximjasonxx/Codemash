using System.Linq;
using Codemash.Api.Data.Repositories.Impl;
using Codemash.Notification;
using Codemash.Notification.Factory;
using Ninject;

namespace Codemash.Poller.Process
{
    public class NotificationCheckProcess : IProcess
    {
        [Inject]
        public INotificationFactory NotificationFactory { get; set; }

        #region Implementation of IProcess

        /// <summary>
        /// Execute the represented process
        /// </summary>
        public void Execute()
        {
            // check each client to make sure they are at the current highest changeset
            using (var context = new CodemashContext())
            {
                int currentChangeset = context.Changes.Max(c => c.Changeset);
                foreach (var client in context.Clients.Where(c => c.CurrentChangeSet != currentChangeset))
                {
                    int changesetDifference = currentChangeset - client.CurrentChangeSet;
                    var notification = NotificationFactory.BuildNotification(client.ChannelUri, changesetDifference);
                }
            }
        }

        #endregion
    }
}
