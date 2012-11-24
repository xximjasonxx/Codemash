using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface IClientRepository
    {
        /// <summary>
        /// Return whether the current Channel URI is already registered with the service
        /// </summary>
        /// <param name="clientUri">The Channel URI identifying the client</param>
        /// <returns></returns>
        bool IsClientRegistered(string clientUri);

        /// <summary>
        /// Register a client for push notifications
        /// </summary>
        /// <param name="client"></param>
        void RegisterClient(Client client);

        /// <summary>
        /// Update the client identified by the channel Uri to the latest changeset
        /// </summary>
        /// <param name="channelUri">The channel URI used to identify a client</param>
        /// <param name="changesetLoaded">The changeset of the most recently loaded changeset</param>
        void UpdateClientChangeset(string channelUri, int changesetLoaded);

        /// <summary>
        /// Return a client instance based on the channel URI to identify it
        /// </summary>
        /// <param name="channel">The channel URI which identifies the client</param>
        /// <returns></returns>
        Client Get(string channel);
    }
}
