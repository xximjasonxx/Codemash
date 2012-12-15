using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Codemash.Client.Common.Services;
using Codemash.Client.Data.Repository;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace Codemash.Client.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        public ISessionRepository SessionRepository { get; private set; }
        public ISpeakerRepository SpeakerRepository { get; private set; }
        public IAppService ApplicationService { get; private set; }
        public IChangeRepository ChangeRepository { get; private set; }
        public IChangeService ChangeService { get; private set; }

        public SplashViewModel(INavigationService navigationService, ISessionRepository sessionRepository, ISpeakerRepository speakerRepository,
            IAppService applicationService, IChangeRepository changeRepository, IChangeService changeService)
            : base(navigationService)
        {
            // assignments
            SessionRepository = sessionRepository;
            SpeakerRepository = speakerRepository;
            ChangeRepository = changeRepository;
            ApplicationService = applicationService;
            ChangeService = changeService;

            LoadStatus = "Preparing Application...";
        }

        protected async override void OnViewReady(object view)
        {
            LoadStatus = "Initializing Push Channel...";
            var result = await ApplicationService.InitalizePushChannel();

            if (result)
            {
                // do the multi load
                if (await LoadRepositoriesAsync())
                {
                    // navigate
                    LoadStatus = "Loading Application...";
                    NavigationService.Navigate(typeof (Views.MainView));
                }
            }
            else
            {
                var dialog = new MessageDialog("The Registration of the Push channel failed. Are you connected to the Internet? The app will now close. Please try again", "Codemash 2.0.1.3");
                await dialog.ShowAsync();
                Application.Current.Exit();
            }
        }

        // attributes
        private string _loadStatus;
        public string LoadStatus
        {
            get { return _loadStatus; }
            private set
            {
                _loadStatus = value;
                NotifyOfPropertyChange();
            }
        }

        // methods
        private async Task<bool> LoadRepositoriesAsync()
        {
            LoadStatus = "Loading Speakers...";
            await SpeakerRepository.LoadAsync();

            LoadStatus = "Loading Sessions...";
            await SessionRepository.LoadAsync();

            LoadStatus = "Getting Changes...";
            await ChangeRepository.LoadAsync();

            var changes = ChangeRepository.GetAll();
            if (changes.Count > 0)
            {
                LoadStatus = "Applying Changes...";
                ChangeService.ApplyChanges(changes);
            }

            return true;
        }
    }
}
