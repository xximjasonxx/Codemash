using System;
using Caliburn.Micro;
using Codemash.Phone7.Data.Repository;
using Ninject;

namespace Codemash.Phone7.App.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        [Inject]
        public ISpeakerRepository SpeakerRepository { get; set; }

        public SplashViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        // behaviors
        public void PageLoaded()
        {
            SessionRepository.LoadCompleted += SessionRepository_LoadCompleted;
            SessionRepository.Load();
        }

        // the load of the Session Repository completed
        private void SessionRepository_LoadCompleted(object sender, EventArgs e)
        {
            SpeakerRepository.LoadCompleted += SpeakerRepository_LoadCompleted;
            SpeakerRepository.Load();
        }

        // the load of the Speaker Repository completed
        private void SpeakerRepository_LoadCompleted(object sender, EventArgs e)
        {
            NavigationService.UriFor<MainViewModel>().Navigate();
        }
    }
}
