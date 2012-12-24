using System;
using System.Windows;
using Caliburn.Micro;
using Codemash.Phone.Data.Repository;
using Codemash.Phone.Shared.Common;
using Codemash.Phone.Shared.Services;
using Ninject;

namespace Codemash.Phone7.App.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        [Inject]
        public ISpeakerRepository SpeakerRepository { get; set; }

        [Inject]
        public IAppService ApplicationService { get; set; }

        public SplashViewModel(INavigationService navigationService) : base(navigationService)
        {
            LoadStatus = "Preparing Application...";
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

        // behaviors
        public void PageLoaded()
        {
            ApplicationService.PushChannelInitialized += ApplicationService_PushChannelInitialized;
            ApplicationService.PushChannelInitializationFailure += ApplicationService_PushChannelInitializationFailure;
            LoadStatus = "Initializing Push Channel...";
            ApplicationService.InitializePushChannel(PhoneClientType.WinPhone7);
        }

        void ApplicationService_PushChannelInitializationFailure(object sender, EventArgs e)
        {
            MessageBox.Show("Push Notification Registration Failed");
            NavigationService.GoBack();
        }

        void ApplicationService_PushChannelInitialized(object sender, EventArgs e)
        {
            SessionRepository.LoadCompleted += SessionRepository_LoadCompleted;
            LoadStatus = "Loading Sessions...";
            SessionRepository.Load();
        }

        // the load of the Session Repository completed
        void SessionRepository_LoadCompleted(object sender, EventArgs e)
        {
            SpeakerRepository.LoadCompleted += SpeakerRepository_LoadCompleted;
            LoadStatus = "Loading Speakers...";
            SpeakerRepository.Load();
        }

        // the load of the Speaker Repository completed
        void SpeakerRepository_LoadCompleted(object sender, EventArgs e)
        {
            LoadStatus = "Loading Application";
            Deployment.Current.Dispatcher.BeginInvoke(() => NavigationService.UriFor<MainViewModel>().Navigate());
        }
    }
}
