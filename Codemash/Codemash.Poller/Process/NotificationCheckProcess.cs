using System.Linq;
using Codemash.Api.Data.Repositories.Impl;
using Codemash.Poller.Notification;
using Ninject;

namespace Codemash.Poller.Process
{
    public class NotificationCheckProcess : IProcess
    {
        [Inject]
        public INotificationManagerResolver NotificationManagerResolver { get; set; }

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
                    var notificationManager = NotificationManagerResolver.Resolve(client.ClientType);
                    notificationManager.SendToast(client.ChannelUri, "New Scheduled Changes", "Press to update");
                }
            }
        }

        #endregion
    }
}
