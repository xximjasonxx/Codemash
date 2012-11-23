
namespace Codemash.Phone.Data.Repository
{
    public interface ISettingsRepository
    {
        /// <summary>
        /// Indicates whether the user has seen the Session List page
        /// </summary>
        bool HasSeenListPage { get; set; }

        /// <summary>
        /// Stores the Push Channel URI for the application which Push will use for communication
        /// </summary>
        string PushChannelUri { get; set; }
    }
}
