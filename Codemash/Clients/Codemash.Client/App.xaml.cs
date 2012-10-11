using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Codemash.Client.Data.Repository;
using Codemash.Client.Data.Repository.Impl;
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
    }
}
