namespace Codemash.Client.Data.Repository
{
    public interface ISettingsRepository
    {
        /// <summary>
        /// Returns the registered Channel URI as stored by application. This can be updated if the URI changes
        /// </summary>
        string RegisteredChannelUri { get; set; }

        /// <summary>
        /// The ID returned by the server following successful registration
        /// </summary>
        int RegisteredClientId { get; set; }
    }
}
