using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Codemash.Client.Data.Repository;

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
        }

        // behaviors
        public async void PageLoaded()
        {
            // do the multi load
            await LoadRepositoriesAsync();
            
            // navigate
            NavigationService.NavigateToViewModel<MainViewModel>();
        }

        // methods
        private async Task LoadRepositoriesAsync()
        {
            await SpeakerRepository.LoadAsync();
            await SessionRepository.LoadAsync();
        }
    }
}
