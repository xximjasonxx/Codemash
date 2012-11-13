using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Codemash.Client.Code;
using Codemash.Client.Components;
using Codemash.Client.Components.Impl;
using Codemash.Client.Data.Repository;
using Codemash.Client.Data.Repository.Impl;
using Codemash.Client.Parameters;
using Codemash.Client.Views;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Search;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Codemash.Client
{
    sealed partial class App
    {
        private WinRTContainer _container;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            base.Configure();

            // create the container
            _container = new WinRTContainer();
            _container.RegisterWinRTServices();

            // repositories
            _container.RegisterSingleton(typeof(ISessionRepository), null, typeof(JsonSessionRepository));
            _container.RegisterSingleton(typeof(ISpeakerRepository), null, typeof(JsonSpeakerRepository));
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            _container.RegisterNavigationService(rootFrame);
            _container.RegisterInstance(typeof(IAppService), null, new CodemashApplicationService(rootFrame));
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<SplashView>();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            if (args.Kind == ActivationKind.Launch)
            {
                OnLaunched((LaunchActivatedEventArgs) args);
                return;
            }

            if (args.Kind == ActivationKind.Search)
            {
                OnSearchActivated((SearchActivatedEventArgs) args);
                return;
            }

            base.OnActivated(args);
        }

        #region Contracts

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            // get the search pane
            var searchPane = SearchPane.GetForCurrentView();

            // get the settings pane
            var settingsPane = SettingsPane.GetForCurrentView();

            // assign the events
            searchPane.ShowOnKeyboardInput = true;
            searchPane.QuerySubmitted += SearchPane_QuerySubmitted;
            settingsPane.CommandsRequested += SettingsPane_CommandsRequested;
        }

        void SettingsPane_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            UICommandInvokedHandler handler = OnSettingsPaneCommand;
            SettingsCommand privacyCommand = new SettingsCommand(Settings.Privacy, "Privacy Policy", handler);
            args.Request.ApplicationCommands.Add(privacyCommand);
        }

        private void SearchPane_QuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        {
            var navService = (INavigationService) _container.GetInstance(typeof (INavigationService), null);
            navService.Navigate(typeof(SearchResultsView), new SearchTextParameter(args.QueryText));

            Window.Current.Activate();
        }

        private void OnSettingsPaneCommand(IUICommand command)
        {
            var cmd = (Settings) command.Id;
            var navService = (INavigationService)_container.GetInstance(typeof(INavigationService), null);
            navService.Navigate(typeof (PrivacyView));
        }

        /// <summary>
        /// Invoked when the application is activated to display search results.
        /// </summary>
        /// <param name="args">Details about the activation request.</param>
        protected override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            DisplayRootView<SearchResultsView>(new SearchTextParameter(args.QueryText));
        }

        #endregion
    }
}
