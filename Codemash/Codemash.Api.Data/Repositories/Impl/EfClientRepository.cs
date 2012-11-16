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

        #endregion
    }
}
