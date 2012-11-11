using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Codemash.Client.Data.Repository;
using Windows.UI.Xaml;

namespace Codemash.Client.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        public ISessionRepository SessionRepository { get; set; }
        public ISpeakerRepository SpeakerRepository { get; set; }

        public SplashViewModel(INavigationService navigationService, ISessionRepository sessionRepository, ISpeakerRepository speakerRepository)
            : base(navigationService)
        {
            // assignments
            SessionRepository = sessionRepository;
            SpeakerRepository = speakerRepository;

            StatusMessage = string.Empty;
            if (SessionRepository.IsFirstLoad)
            {
                StatusMessage = "Loading Sessions for the first time";
            }
        }

        protected async override void OnViewReady(object view)
        {
            // do the multi load
            await LoadRepositoriesAsync();
            
            // navigate
            NavigationService.NavigateToViewModel<MainViewModel>();
        }

        // attributes
        public string StatusMessage { get; set; }

        // methods
        private async Task LoadRepositoriesAsync()
        {
            await SpeakerRepository.LoadAsync();
            await SessionRepository.LoadAsync();
        }
    }
}
