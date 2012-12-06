using System.Threading.Tasks;

namespace Codemash.Client.Common.Services
{
    public interface IAppService
    {
        /// <summary>
        /// Application Service property indicating whether back is an option
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Initialize the Push channel to allow the device to receive notifications
        /// </summary>
        Task InitalizePushChannel();
    }
}
