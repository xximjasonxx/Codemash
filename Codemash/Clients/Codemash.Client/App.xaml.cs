using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Codemash.Client.Data.Repository;
using Codemash.Client.Data.Repository.Impl;
using Codemash.Client.Parameters;
using Codemash.Client.ViewModels;

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
            _container = new WinRTContainer(RootFrame);
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

        #region Overrides of CaliburnApplication

        protected override Type GetDefaultViewModel()
        {
            return typeof (SplashViewModel);
        }

        #endregion

        #region Search

        /// <summary>
        /// Invoked when the application is activated to display search results.
        /// </summary>
        /// <param name="args">Details about the activation request.</param>
        protected override void OnSearchActivated(Windows.ApplicationModel.Activation.SearchActivatedEventArgs args)
        {
            var navigationService = (INavigationService) _container.GetInstance(typeof (INavigationService), null);
            navigationService.NavigateToViewModel<SearchResultsViewModel>(new SearchTextParameter(args.QueryText));
        }

        #endregion
    }
}
