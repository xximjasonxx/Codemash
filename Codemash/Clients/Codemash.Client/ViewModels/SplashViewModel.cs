using System.Threading.Tasks;
using Caliburn.Micro;
using Codemash.Client.Common.Services;
using Codemash.Client.Data.Repository;

namespace Codemash.Client.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        public ISessionRepository SessionRepository { get; private set; }
        public ISpeakerRepository SpeakerRepository { get; private set; }
        public IAppService ApplicationService { get; private set; }

        public SplashViewModel(INavigationService navigationService, ISessionRepository sessionRepository, ISpeakerRepository speakerRepository,
            IAppService applicationService)
            : base(navigationService)
        {
            // assignments
            SessionRepository = sessionRepository;
            SpeakerRepository = speakerRepository;
            ApplicationService = applicationService;

            LoadStatus = "Preparing Application...";
        }

        protected async override void OnViewReady(object view)
        {
            LoadStatus = "Initializing Push Channel";
            await ApplicationService.InitalizePushChannel();

            // do the multi load
            await LoadRepositoriesAsync();
            
            // navigate
            NavigationService.Navigate(typeof(Views.MainView));
        }

        // attributes
        private string _loadStatus;
        public string LoadStatus
        {
            get { return _loadStatus; }
            private set
            {
                _loadStatus = value;
                NotifyOfPropertyChange("LoadStatus");
            }
        }

        // methods
        private async Task LoadRepositoriesAsync()
        {
            LoadStatus = "Loading Speakers...";
            await SpeakerRepository.LoadAsync();

            LoadStatus = "Loading Sessions...";
            await SessionRepository.LoadAsync();
        }
    }
}
