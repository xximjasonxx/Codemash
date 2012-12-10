using System.Threading.Tasks;

namespace Codemash.Client.Common.Services
{
    public interface IAppService
    {
        /// <summary>
        /// Initialize the Push channel to allow the device to receive notifications
        /// </summary>
        Task<bool> InitalizePushChannel();
    }
}
