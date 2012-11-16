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
    }
}
