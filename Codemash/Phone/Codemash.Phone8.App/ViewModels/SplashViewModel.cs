using System;
using System.Windows;
using Codemash.Phone.Data.Provider;
using Codemash.Phone.Shared.Common;
using Ninject;
using Caliburn.Micro;
using Codemash.Phone.Data.Repository;
using Codemash.Phone.Shared.Services;

namespace Codemash.Phone8.App.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        [Inject]
        public ISpeakerRepository SpeakerRepository { get; set; }

        [Inject]
        public IChangeProvider ChangeProvider { get; set; }

        [Inject]
        public IAppService ApplicationService { get; set; }

        [Inject]
        public IChangeRepository ChangeRepository { get; set; }

        public SplashViewModel(INavigationService navigationService)
            : base(navigationService)
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
            LoadStatus = "Initializing Push Channel...";
            ApplicationService.InitializePushChannel(PhoneClientType.WinPhone8);
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
            ChangeRepository.LoadCompleted += ChangeRepository_LoadCompleted;
            LoadStatus = "Checking for Changes...";
            ChangeRepository.Load();
        }

        // load of the pending changes is completed (not applied)
        void ChangeRepository_LoadCompleted(object sender, EventArgs e)
        {
            LoadStatus = "Applying Changes...";
            ChangeProvider.ApplyChanges(ChangeRepository.GetAll());

            LoadStatus = "Loading Application";
            Deployment.Current.Dispatcher.BeginInvoke(() => NavigationService.UriFor<MainViewModel>().Navigate());
        }
    }
}