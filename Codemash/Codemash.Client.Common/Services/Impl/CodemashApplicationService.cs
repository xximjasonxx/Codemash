
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Codemash.Client.Common.Services.Impl
{
    public class CodemashApplicationService : IAppService
    {
        private readonly Frame _applicationFrame;

        public CodemashApplicationService(Frame applicationFrame)
        {
            _applicationFrame = applicationFrame;
        }

        #region Implementation of IAppService

        /// <summary>
        /// Application Service property indicating whether back is an option
        /// </summary>
        public bool CanGoBack { get { return _applicationFrame.CanGoBack; } }

        /// <summary>
        /// Initialize the Push channel to allow the device to receive notifications
        /// </summary>
        public Task InitalizePushChannel()
        {
            return null;
        }

        #endregion
    }
}
