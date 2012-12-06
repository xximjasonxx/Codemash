using Caliburn.Micro;
using Codemash.Client.Common.Services;
using Codemash.Client.Components;

namespace Codemash.Client.ViewModels
{
    public class ViewModelBase : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IAppService _appService;

        protected INavigationService NavigationService
        {
            get { return _navigationService; }
        }

        protected ViewModelBase(INavigationService navigationService, bool canGoBack)
        {
            _navigationService = navigationService;
            _canGoBack = canGoBack;
        }

        protected ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _canGoBack = true;
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        private readonly bool _canGoBack = false;
        public bool BackIsVisible
        {
            get { return _canGoBack; }
        }
    }
}
