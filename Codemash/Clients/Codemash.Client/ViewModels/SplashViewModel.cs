using System;
using Caliburn.Micro;
using Codemash.Client.Data.Repository;

namespace Codemash.Client.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        private bool _sessionsLoaded = false;
        private bool _speakersLoaded = false;

        public ISessionRepository SessionRepository { get; set; }
        public ISpeakerRepository SpeakerRepository { get; set; }

        public SplashViewModel(INavigationService navigationService, ISessionRepository sessionRepository, ISpeakerRepository speakerRepository)
            : base(navigationService)
        {
            // assignments
            SessionRepository = sessionRepository;
            SpeakerRepository = speakerRepository;

            // events
            SessionRepository.LoadCompleted += SessionRepositoryOnLoadCompleted;
            SpeakerRepository.LoadCompleted += SpeakerRepositoryOnLoadCompleted;
        }

        private void SpeakerRepositoryOnLoadCompleted(object sender, EventArgs eventArgs)
        {
            _speakersLoaded = true;
            NavigateToMainViewModel();
        }

        private void SessionRepositoryOnLoadCompleted(object sender, EventArgs eventArgs)
        {
            _sessionsLoaded = true;
            NavigateToMainViewModel();
        }

        private void NavigateToMainViewModel()
        {
            if (_sessionsLoaded && _speakersLoaded)
            {
                NavigationService.NavigateToViewModel<MainViewModel>();
            }
        }

        // behaviors
        public void PageLoaded()
        {
            SpeakerRepository.Load();
            SessionRepository.Load();
        }
    }
}
