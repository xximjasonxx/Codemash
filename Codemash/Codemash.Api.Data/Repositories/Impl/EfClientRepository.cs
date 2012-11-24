using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories.Impl
{
    public class EfClientRepository : IClientRepository
    {
        #region Implementation of IClientRepository

        /// <summary>
        /// Return whether the current Channel URI is already registered with the service
        /// </summary>
        /// <param name="clientUri">The Channel URI identifying the client</param>
        /// <returns></returns>
        public bool IsClientRegistered(string clientUri)
        {
            using (var context = new CodemashContext())
            {
                return context.Clients.FirstOrDefault(
                    c => string.Compare(c.ChannelUri, clientUri, StringComparison.OrdinalIgnoreCase) == 0) != null;
            }
        }

        /// <summary>
        /// Register a client for push notifications
        /// </summary>
        /// <param name="client"></param>
        public void RegisterClient(Client client)
        {
            using (var context = new CodemashContext())
            {
                // determine the latest changeset number
                var changeset = 0;
                if (context.Changes.Any())
                    changeset = context.Changes.Max(c => c.Changeset);

                client.CurrentChangeSet = changeset;
                context.Clients.Add(client);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Update the client identified by the channel Uri to the latest changeset
        /// </summary>
        /// <param name="channelUri">The channel URI used to identify a client</param>
        /// <param name="changesetLoaded">The changeset of the most recently loaded changeset</param>
        public void UpdateClientChangeset(string channelUri, int changesetLoaded)
        {
            using (var context = new CodemashContext())
            {
                var client = context.Clients.FirstOrDefault(c => c.ChannelUri == channelUri);
                if (client != null)
                {
                    client.CurrentChangeSet = changesetLoaded;
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Return a client instance based on the channel URI to identify it
        /// </summary>
        /// <param name="channel">The channel URI which identifies the client</param>
        /// <returns></returns>
        public Client Get(string channel)
        {
            using (var context = new CodemashContext())
            {
                return context.Clients.FirstOrDefault(c => c.ChannelUri == channel);
            }
        }

        #endregion
    }
}
