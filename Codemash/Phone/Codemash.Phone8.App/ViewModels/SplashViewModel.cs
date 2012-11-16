using System;
using Ninject;
using Caliburn.Micro;
using Codemash.Phone.Data.Repository;
using Codemash.Phone.Shared.Services;

namespace Codemash.Phone8.App.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        private const string ClientTypeName = "WinPhone8";

        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        [Inject]
        public ISpeakerRepository SpeakerRepository { get; set; }

        [Inject]
        public IAppService ApplicationService { get; set; }

        public SplashViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            
        }

        // behaviors
        public void PageLoaded()
        {
            ApplicationService.PushChannelInitialized += ApplicationService_PushChannelInitialized;
            ApplicationService.InitializePushChannel();
        }

        void ApplicationService_PushChannelInitialized(object sender, EventArgs e)
        {
            SessionRepository.LoadCompleted += SessionRepository_LoadCompleted;
            SessionRepository.Load(); 
        }

        // the load of the Session Repository completed
        void SessionRepository_LoadCompleted(object sender, EventArgs e)
        {
            SpeakerRepository.LoadCompleted += SpeakerRepository_LoadCompleted;
            SpeakerRepository.Load();
        }

        // the load of the Speaker Repository completed
        void SpeakerRepository_LoadCompleted(object sender, EventArgs e)
        {
            NavigationService.UriFor<MainViewModel>().Navigate();
        }
    }
}