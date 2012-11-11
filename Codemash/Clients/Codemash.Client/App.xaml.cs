using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Codemash.Client.Components;
using Codemash.Client.Components.Impl;
using Codemash.Client.Data.Repository;
using Codemash.Client.Data.Repository.Impl;
using Codemash.Client.Parameters;
using Codemash.Client.ViewModels;
using Codemash.Client.Views;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Search;
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

        #region Search

        protected override void OnWindowCreated(Windows.UI.Xaml.WindowCreatedEventArgs args)
        {
            // get the search pane
            var searchPane = SearchPane.GetForCurrentView();

            // assign the events
            searchPane.ShowOnKeyboardInput = true;
            searchPane.QuerySubmitted += searchPane_QuerySubmitted;
        }

        private void searchPane_QuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        {
            var navService = (INavigationService) _container.GetInstance(typeof (INavigationService), null);
            navService.NavigateToViewModel<SearchResultsViewModel>(new SearchTextParameter(args.QueryText));

            Window.Current.Activate();
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
