using System.Threading.Tasks;
using Codemash.Client.Common.Result;

namespace Codemash.Client.Common.Services
{
    public interface INotificationRegistrationService
    {
        /// <summary>
        /// Register the Channel URI for Push with the Notification Service
        /// </summary>
        /// <param name="channelUri"></param>
        /// <returns></returns>
        Task<RegistrationResult> Register(string channelUri);
    }
}
