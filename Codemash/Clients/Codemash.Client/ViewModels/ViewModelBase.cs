using Caliburn.Micro;

namespace Codemash.Client.ViewModels
{
    public class ViewModelBase : Screen
    {
        private readonly INavigationService _navigationService;
        protected INavigationService NavigationService
        {
            get { return _navigationService; }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        public bool CanGoBack
        {
            get { return _navigationService.CanGoBack; }
        }
    }
}
